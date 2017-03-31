using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace SE407_isabella
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //adding entity framework
            #region bridges
            services.AddEntityFrameworkSqlServer()
                .AddEntityFrameworkSqlServer()
            
                .AddDbContext<SE407_isabella.BridgeDBContext>(options =>
                {
                    options.UseSqlServer(
                        Configuration.GetConnectionString("MSSQLDB"));
                });
            #endregion

            //#region constructiondesign
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.ConstructionDesignsDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            //#region counties
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.CountiesDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            //#region functionalclasses
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.FunctionalClassesDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            //#region inspectioncodes
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.InspectionCodesDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            //#region inspections
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.InspectionsDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            //#region inspectors
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.InspectorsDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            //#region maintenanceactions
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.MaintenanceRecordsDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            //#region maintenancerecords
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.MaintenanceRecordsDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            //#region materialdesigns
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.MaterialDesignsDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            //#region statuscodes
            //services.AddEntityFrameworkSqlServer()
            //    .AddEntityFrameworkSqlServer()

            //    .AddDbContext<SE407_isabella.StatusCodesDBContext>(options =>
            //    {
            //        options.UseSqlServer(
            //            Configuration.GetConnectionString("MSSQLDB"));
            //    });
            //#endregion

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {   
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

           // app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

           // app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
