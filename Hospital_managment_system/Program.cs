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
                    LastName = "Gates",
                    Age = 46,
                    Department = "Last",
                    UserName = "doctor@test.com",
                    Email = "doctor@test.com",
                    Patients = new HashSet<Patient>()
                };
                userMgr.CreateAsync(doctor, "Password123").GetAwaiter().GetResult();
                userMgr.AddToRoleAsync(doctor, "Doctor").GetAwaiter().GetResult();

                //add patient user
                var patient = new Patient()
                {
                    FirstName = "Bill",
                    LastName = "Smith",
                    Age = 32,
                    Chamber = 18,
                    Diagnosis = "Dead",
                    Email = "patient123@test.com",
                    UserName = "patient123@test.com",
                    DoctorId = ctx.Users.FirstOrDefault(u => u.UserName == "doctor@test.com").Id,
                    Doctor = doctor,
                    Consultations = new HashSet<Consultation>()
                };
                userMgr.CreateAsync(patient, "Password123").GetAwaiter().GetResult();
                userMgr.AddToRoleAsync(patient, "Patient").GetAwaiter().GetResult();

                //add firt post
                ctx.Posts.Add(new Post()
                {
                    Header = "Something important",
                    Body = "<p>Lorem ipsum dolor sit amet, mundi nobis an sed, duo quot consul nostro cu. Nostrum splendide cum at, duo viderer verterem ne. Pri sint decore interpretaris et, no reprehendunt interpretaris usu. Deleniti complectitur per ex, te solum soleat has. Ut numquam pertinacia his.</p><p><br></p><p> Vel ut mazim quaestio theophrastus,vim agam rebum id.Quis augue necessitatibus has ut.At his assentior complectitur,mediocrem explicari ex eum.Nec consul quaeque accumsan et,odio affert feugait qui ut.</p><p><br></p><p> Autem option appetere sit an,ne has solum paulo euismod.Inani tractatos dignissim vix ex.Sea an accumsan singulis,ne usu argumentum ullamcorper,paulo blandit lucilius est ex.Usu et nibh habeo doctus,vim id quod partem suscipit.Quaestio suavitate et eum,alii noluisse intellegam in nec,qui at commune molestiae.Cum ad porro praesent,est partiendo interesset ea.</p><p><br></p><p> Quando incorrupte ius cu.Novum munere cu vim,suscipit iudicabit et nam.Eum ad meis quando recteque,choro deserunt an eos,vel nulla civibus referrentur ea.Ne nemore pertinax eam.Sed sanctus debitis voluptua no,tibique vivendum ne ius.</p><p><br></p><p> Ne modus clita abhorreant eos,eum dicit maiestatis te.Cu scaevola scriptorem duo,eam dicunt prodesset ullamcorper an.Ne aeque phaedrum menandri his.Id has iisque blandit,est te errem nonumy facete.Simul similique suscipiantur mea an,ex eam assum maiestatis,principes reprimique intellegam vis ad.</p>",
                    Created = DateTime.Now
                });
                ctx.SaveChanges();

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
