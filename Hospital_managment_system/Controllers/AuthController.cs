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
        private IRepository repo;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userMgr, IRepository repo)
        {
            this.signInManager = signInManager;
            this.userMgr = userMgr;
            this.repo = repo;
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
                var user = repo.GetUserByUserNameAsync(vm.Email);
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