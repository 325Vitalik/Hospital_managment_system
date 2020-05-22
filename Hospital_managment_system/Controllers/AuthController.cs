using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Data.Repository;
using DataBase.Models;
using Hospital_managment_system.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_managment_system.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userMgr;
        private IUserRepository userRepo;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userMgr, IUserRepository userRepo)
        {
            this.signInManager = signInManager;
            this.userMgr = userMgr;
            this.userRepo = userRepo;
        }

        //Logining
        [HttpGet]
        public IActionResult SignIn()
        {
            return View("Login", new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //TODO: Написати перевірки
                var user = userRepo.GetUserByUserNameAsync(vm.Email);
                var result = await signInManager.PasswordSignInAsync(await user, vm.Password, false, false);
                if (result == null) return View(vm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}