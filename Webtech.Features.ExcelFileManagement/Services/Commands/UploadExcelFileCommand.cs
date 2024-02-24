using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webtech.Domain.Entities;
using Webtech.Infrastructure.UnitOfWork;
using Webtech.Shared.DTOs;

namespace Webtech.Features.ExcelFileManagement.Services.Commands
{
    public class UploadExcelFileCommand : IRequest<Result<List<Dictionary<string, object>>>>
    {
        public UploadExcelFileCommand(UploadFileRequest Request)
        {
            this.ExcelFile = Request;
        }
        public UploadFileRequest ExcelFile { get; set; }

        /// <summary>
        /// Handler for command.
        /// </summary>
        public class Handler : BaseService, IRequestHandler<UploadExcelFileCommand, Result<List<Dictionary<string, object>>>>
        {
            private const string SuccessMessage = "Success uploading excel file.";
            private const string FailedMessage1 = "Failed to upload excel file.";
            private const string FailedMessage2 = "File is empty.";
            private const string FailedMessage3 = "Only excel files are allowed. ";

            private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="excelUnitOfWork">User unit of work.</param>
            /// <param name="mapper">THe mapper.</param>
            public Handler(IExcelUnitOfWork excelUnitOfWork, IMapper mapper)
                : base(excelUnitOfWork, mapper)
            {
            }

            /// <inheritdoc/>
            public async Task<Result<List<Dictionary<string, object>>>> Handle(UploadExcelFileCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var file = command.ExcelFile.File;

                    //validations
                    if (file == null || file.Length == 0)
                        return Result<List<Dictionary<string, object>>>.Fail(FailedMessage2);

                    if (file.ContentType != ExcelContentType)
                        return Result<List<Dictionary<string, object>>>.Fail(FailedMessage3);



                    var result = new List<Dictionary<string, object>>();
                    using (var stream = file.OpenReadStream())
                    {
                        var workbook = new XSSFWorkbook(stream);

                        if (workbook.NumberOfSheets == 0)
                            return Result< List<Dictionary<string, object>>>.Fail(FailedMessage2);

                        var sheet = workbook.GetSheetAt(0); // Assuming data is in the first sheet

                        

                        // Iterate over rows and columns to extract data
                        for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                        {
                            var row = sheet.GetRow(i);
                            if (row == null) continue;

                            var rowData = new Dictionary<string, object>();

                            // Iterate over cells in the row
                            for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                            {
                                var cell = row.GetCell(j);
                                var columnName = $"Column{j + 1}"; // Generate column name if not present
                                if (sheet.GetRow(sheet.FirstRowNum) != null && sheet.GetRow(sheet.FirstRowNum).GetCell(j) != null)
                                {
                                    columnName = sheet.GetRow(sheet.FirstRowNum).GetCell(j).ToString();
                                }

                                rowData[columnName] = cell == null ? null : GetCellValue(cell);
                            }

                            result.Add(rowData);
                        }
                    }

                    // saving file into database if needed for future reference
                    byte[] fileBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }

                    var fileEntity = new FileEntity
                    {
                        Filename = file.FileName,
                        Content = fileBytes,
                        Size = fileBytes.Length,
                        DateCreated = DateTime.Now,

                    };

                    await this.ExcelUnitOfWork.FilesRepository.AddAsync(fileEntity);
                    await this.ExcelUnitOfWork.Save();

                    return Result<List<Dictionary<string, object>>>.Success(result, SuccessMessage);
                }
                catch (Exception ex)
                {
                    return Result<List<Dictionary<string, object>>>.Fail(ex.Message);
                }
            }

            private object GetCellValue(ICell cell)
            {
                switch (cell.CellType)
                {
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                            return cell.DateCellValue;
                        else
                            return cell.NumericCellValue;
                    case CellType.Boolean:
                        return cell.BooleanCellValue;
                    case CellType.String:
                        return cell.StringCellValue;
                    case CellType.Formula:
                        return cell.CellFormula;
                    case CellType.Blank:
                    case CellType.Unknown:
                    default:
                        return null;
                }
            }
        }
    }
}
