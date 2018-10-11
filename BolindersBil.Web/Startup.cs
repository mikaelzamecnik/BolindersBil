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
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;
using BolindersBil.Web.Models.NewsModels;

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

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add framework services
            services.AddMvc();
            services.AddTransient<ArticlesResult>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn));

            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>().
                AddDefaultTokenProviders();

            services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            });

            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.Configure<CustomAppSettings>(_configuration.GetSection("CustomAppSettings"));
            // Make all generated URLs lowercase
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext ctx, IIdentitySeeder identitySeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                
            }
            else
            {
                app.UseExceptionHandler("/error.html");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseCookiePolicy();
            //app.UseMvcWithDefaultRoute();

            // Add custom route template

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseMvc(routes =>
            {
                // -> /bilar/1
                routes.MapRoute(
                    name: "bilar/vehicleId",
                    template: "bilar/{brandName}/{model}/{modelDescription}/{vehicleId:int}",
                    defaults: new { controller = "Home", action = "Vehicle" }
                );

                // -> /bilar/nya och bilar/begagnade
                routes.MapRoute(
                    name: "bilar/state",
                    template: "bilar/{state}",
                    defaults: new { controller = "Home", action = "Index" }
                );

                // -> /bilar
                routes.MapRoute(
                    name: "",
                    template: "bilar",
                    defaults: new { controller = "Home", action = "Index" }
                );

                // -> /
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"
                );
            });

            var runIdentitySeed = Task.Run(async () => await identitySeeder.CreateAdminAccountIfEmpty()).Result;

            VehicleSeed.FillIfEmpty(ctx);
        }
    }
}
