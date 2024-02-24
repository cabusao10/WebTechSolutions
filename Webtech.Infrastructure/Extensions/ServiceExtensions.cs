using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webtech.Infrastructure.Context;
using Webtech.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Webtech.Infrastructure.UnitOfWork;

namespace Webtech.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddDbContext<WebtechContext>((provider, options) =>
            {
                options.UseSqlServer(
                    configuration["DefaultConnection"],
                    b => b.MigrationsAssembly(typeof(WebtechContext).Assembly.FullName));
                options.UseLazyLoadingProxies(true);

            });

            services.AddScoped<IWebtechContext, WebtechContext>();

            services.AddTransient(typeof(IApplicationRepository<>), typeof(ApplicationRepository<>));
            services.AddTransient<IExcelUnitOfWork, ExcelUnitOfWork>();


        }
    }
}
