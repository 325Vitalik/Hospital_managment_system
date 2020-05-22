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
        private IPostRepository postRepo;
        public HomeController(IPostRepository postRepo)
        {
            this.postRepo = postRepo;
        }
        public IActionResult Index()
        {
            var posts = postRepo.GetAllPosts();

            return View(posts);
        }

        [Route("/Denied")]
        public IActionResult Denied()
        {
            return View("Denied");
        }
    }
}