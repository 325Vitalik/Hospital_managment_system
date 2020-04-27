using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Data;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hospital_managment_system
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();

            var adminRole = new IdentityRole("Admin");
            var doctorRole = new IdentityRole("Doctor");
            var patientRole = new IdentityRole("Patient");

            if (!ctx.Roles.Any())
            {
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                roleMgr.CreateAsync(doctorRole).GetAwaiter().GetResult();
                roleMgr.CreateAsync(patientRole).GetAwaiter().GetResult();
            }
            if (!ctx.Users.Any(u => u.UserName == "admin"))
            {
                //add admin user
                var adminUser = new User()
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com"
                };
                userMgr.CreateAsync(adminUser, "Password123").GetAwaiter().GetResult();

                userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

                //add doctor user
                var doctor = new Doctor()
                {
                    FirstName = "Bob",
                    LastName = "gates",
                    Age = 46,
                    Department = "Last",
                    UserName = "doctor@test.com",
                    Email = "doctor@test.com",
                    Patients = new HashSet<Patient>()
                };
                userMgr.CreateAsync(doctor, "Password123").GetAwaiter().GetResult();

                userMgr.AddToRoleAsync(adminUser, "Doctor").GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
