using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Data.Repository;
using DataBase.Models;
using Hospital_managment_system.ViewModels;
using Hospital_managment_system.ViewModels.DoctorViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_managment_system.Controllers
{
    [Route("/Register/Doctor")]
    [Authorize(Roles="Admin")]
    public class DoctorAuthController : Controller
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userMgr;
        private IRepository repo;

        public DoctorAuthController(SignInManager<User> signInManager, UserManager<User> userMgr, IRepository repo)
        {
            this.signInManager = signInManager;
            this.userMgr = userMgr;
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Doctors = repo.GetAllDoctors();
            return View(new DoctorSignUpViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(DoctorSignUpViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var doctor = new Doctor()
                {
                    UserName = vm.Email,
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Age = vm.Age,
                    Department = vm.Department,
                    Patients = vm.Patients
                };

                var result = await userMgr.CreateAsync(doctor, vm.Password);
                await userMgr.AddToRoleAsync(doctor, "Doctor");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(vm);
            }
        }
    }
}