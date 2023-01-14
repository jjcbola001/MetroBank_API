using Metrobank.DataAccessLibrary.DataAccess;
using Metrobank.DataAccessLibrary.Interfaces;
using Metrobank.Interface.Interfaces;
using Metrobank.Model.SharedModel;
using Metrobank.Repository.Repository;
using Metrobank.Services.Services;
using Metrobank.SharedUtilities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;


namespace Metrobank.ManageDependency.Dependecy
{
    public static class DIContainer
    {
        public static void ConfigureIOC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<MetrobankDataAccess>(
                options => options.UseSqlServer(configuration.GetConnectionString("PrimaryDatabaseConnectionString"))
                );

            services.AddScoped<IMetrobankRepository, MetrobankRepositry>();
            services.AddScoped<IMetrobankServices, MetrobankServices>();
            services.AddTransient<IMetrobankDataAccess, MetrobankDataAccess>();
            services.AddTransient(typeof(ILoggerService<>), typeof(LoggerService<>));
        }
    }
}
