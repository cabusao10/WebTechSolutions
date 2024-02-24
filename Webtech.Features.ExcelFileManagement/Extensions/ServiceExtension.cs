
namespace Webtech.Features.ExcelFileManagement.Extensions
{
    using System.Reflection;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// User management extensions.
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Service extension for excek file management layer.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddExcelFileManagementLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
