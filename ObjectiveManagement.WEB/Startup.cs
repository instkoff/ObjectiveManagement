using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ObjectiveManagement.Domain.Contracts;
using ObjectiveManagement.Domain.Implementations;
using ObjectiveManagement.Web;
using Serilog;

namespace ObjectiveManagement.WEB
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
            services.AddLocalization(opt => opt.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddViewLocalization()
                .AddNewtonsoftJson();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwagger();
            services.AddDatabase(Configuration);
            services.AddTransient<IObjectiveService, ObjectiveService>();
            services.AddTransient<IMenuService, MenuService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            //     app.UseDeveloperExceptionPage();
            // else
                app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Objective Management v1");
                x.RoutePrefix = "swagger";
            });

             app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
