using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using DataBase.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Hospital_managment_system.ViewModels;

namespace Hospital_managment_system.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<User> userMgr;
        private IRepository repo;
        public HomeController(IRepository repo, UserManager<User> userMgr)
        {
            this.userMgr = userMgr;
            this.repo = repo;
        }
        public IActionResult Index()
        {
            var posts = repo.GetAllPosts();

            return View(posts);
        }

        [Route("/Denied")]
        public IActionResult Denied()
        {
            return View("Denied");
        }
    }
}