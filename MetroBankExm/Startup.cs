using Metrobank.DataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Metrobank.ManageDependency.Dependecy;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Metrobank.Model.SharedModel;

namespace MetroBankExm
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(
                       origin => origin.AllowAnyOrigin()
                );

                option.AddPolicy(name: "Government Exception",
                      origin =>
                      {
                          origin
                          .WithOrigins("https://124.83.13.196", "http://10.10.5.42")
                          .WithMethods("POST")
                          .AllowAnyHeader();
                      }
                    );

                option.AddPolicy(name: "Private Exception",
                   origin =>
                   {
                       origin
                       .WithOrigins("https://124.83.13.196", "http://10.10.5.42")
                       .WithMethods("POST")
                       .AllowAnyHeader();
                   }
                 );
            });
            services.ConfigureIOC(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.Configure<FilePath>(Configuration.GetSection("FilePath"));
            services.Configure<LogResult>(Configuration.GetSection("Logresult"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(swag =>
            {
                swag.SwaggerEndpoint("/swagger/v1/swagger.json", "Metro Bank Examnination");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Government Exception");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
