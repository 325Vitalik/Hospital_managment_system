﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Data.Repository;
using DataBase.Models;
using Hospital_managment_system.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_managment_system.Controllers
{
    [Route("Post")]
    public class PostController : Controller
    {
        private IPostRepository postRepo;

        public PostController(IPostRepository repo)
        {
            this.postRepo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Post(long id)
        {
            var post = postRepo.GetPostByIdAsync(id);
            return View(await post);
        }

        [Authorize(Roles = "Admin")]
        [Route("CreatePost")]
        public IActionResult CreatePost()
        {
            return View(new PostViewModel());
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(PostViewModel vm)
        {
            var post = new Post()
            {
                Header = vm.Header,
                Body = vm.Body,
                Created = DateTime.Now
            };
            postRepo.AddPost(post);
            await postRepo.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}