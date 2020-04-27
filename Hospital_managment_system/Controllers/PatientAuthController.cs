using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Data;
using DataBase.Data.Repository;
using DataBase.Models;
using Hospital_managment_system.ViewModels;
using Hospital_managment_system.ViewModels.PatientViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_managment_system.Controllers
{
    //only doctor can add new patient
    [Authorize(Roles = "Doctor")]
    public class PatientAuthController : Controller
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userMgr;
        private IRepository repo;

        public PatientAuthController(SignInManager<User> signInManager, UserManager<User> userMgr, IRepository repo)
        {
            this.signInManager = signInManager;
            this.userMgr = userMgr;
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Doctors = repo.GetAllDoctors();
            return View(new PatientSignUpViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(PatientSignUpViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var doctor = await repo.GetDoctorByUserNameAsync(User.Identity.Name);
                var patient = new Patient()
                {
                    UserName = vm.Email,
                    Email = vm.Email,
                    Age = vm.Age,
                    Chamber = vm.Chamber,
                    Diagnosis = vm.Diagnosis,
                    DoctorId = doctor.Id,
                    Doctor = doctor,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName
                };

                var result = await userMgr.CreateAsync(patient, vm.Password);
                await userMgr.AddToRoleAsync(patient, "Patient");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(vm);
            }
        }
    }
}