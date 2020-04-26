using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataBase.Data;
using Microsoft.AspNetCore.Identity;
using DataBase.Models;
using DataBase.Data.Repository;

namespace Hospital_managment_system
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HospitalDbContext>(options => options.UseSqlServer(_config["DefaultConnection"],
                                                    b => b.MigrationsAssembly("DataBase")));

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                }).AddEntityFrameworkStores<HospitalDbContext>();

            services.AddTransient<IRepository, Repository>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Denied";
                options.AccessDeniedPath = "/Denied";
            });

            //services.AddControllersWithViews();
            services.AddMvc(options => options.EnableEndpointRouting=false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
