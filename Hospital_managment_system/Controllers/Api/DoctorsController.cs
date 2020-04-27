using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataBase.Data.Repository;
using DataBase.Models;
using Hospital_managment_system.ViewModels.PatientViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hospital_managment_system.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private IRepository repo;
        private SignInManager<User> signInMgr;
        private UserManager<User> userMgr;

        public DoctorsController(IRepository repo, SignInManager<User> signInManager, UserManager<User> userMgr)
        {
            this.repo = repo;
            this.signInMgr = signInManager;
            this.userMgr = userMgr;
        }

        // GET: api/Doctors
        // Return: json with all doctors
        [HttpGet]
        public ActionResult<string> GetAllDoctors()
        {
            var doctors = repo.GetAllDoctors();
            if (doctors == null) return NotFound();

            return JsonSerializer.CreateJson(doctors);
        }

        // GET: api/Doctors/ById/{id}
        // Return json with one docotor data
        [Route("ById/{id}")]
        [HttpGet]
        public async Task<ActionResult<string>> GetDoctorById(string id)
        {
            var doctor = await repo.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();


            return JsonSerializer.CreateJson(doctor);
        }

        // GET: api/Doctors/ByUserName/{username}
        // Return json with one docotor data
        [Route("ByUserName/{username}")]
        [HttpGet]
        public async Task<ActionResult<string>> GetDoctorByUserName(string userName)
        {
            var doctor = await repo.GetDoctorByUserNameAsync(userName);
            if (doctor == null) return NotFound();

            return JsonSerializer.CreateJson(doctor);
        }

        // POST: api/Doctors/AddPatient/{email}/{password}
        // returns message if action is successful
        [Route("AddPatient/{email}/{password}")]
        [HttpPost]
        public async Task<IActionResult> Post(string email, string password, [FromBody] PatientSignUpViewModel value)
        {
            var doctor = await repo.GetDoctorByUserNameAsync(email);

            if (doctor == null) return StatusCode(403, "User doesn't exist");
            var result = await this.signInMgr.PasswordSignInAsync(doctor, password, false, false);
            if (!result.Succeeded) return StatusCode(403, "Incorrect password");

            var patient = new Patient()
            {
                UserName = value.Email,
                Email = value.Email,
                Age = value.Age,
                Chamber = value.Chamber,
                Diagnosis = value.Diagnosis,
                DoctorId = doctor.Id,
                Doctor = doctor,
                FirstName = value.FirstName,
                LastName = value.LastName
            };

            await userMgr.CreateAsync(patient, value.Password);
            await userMgr.AddToRoleAsync(patient, "Patient");
            return StatusCode(200, "Success");
        }
    }
}
