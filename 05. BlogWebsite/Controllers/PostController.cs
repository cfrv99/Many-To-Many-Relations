using BlogWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogDbContext context;

        public PostController(BlogDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(context.Posts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            //ModelState.AddModelError("Title", "Error!!!!");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Date = DateTime.Now;
                context.Posts.Add(post);
                await context.SaveChangesAsync();
                TempData["Status"] = "New post added!";
                return RedirectToAction("Index");
            }
            return View(post);
        }
    }
}
