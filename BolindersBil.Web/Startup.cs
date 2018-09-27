using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.DataAccess;
using BolindersBil.Web.Repositories;
using BolindersBil.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BolindersBil.Web
{
    public class Startup
    {


        IConfiguration _configuration;

        public Startup(IConfiguration conf)
        {
            _configuration = conf;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            var conn = _configuration.GetConnectionString("BolindersBil");
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn));
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.Configure<CustomAppSettings>(_configuration.GetSection("CustomAppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            // Add custom route template

            //app.UseMvc(routes =>
            //{

            //    routes.MapRoute(

            //       name: "somename",
            //       template: "home/page/{page}",
            //       defaults: new { Controller = "Home", action = "List" });
            //});
            VehicleSeed.FillIfEmpty(ctx);
        }
    }
}
