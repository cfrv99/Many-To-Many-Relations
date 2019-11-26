using BlogWebsite.Helpers;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogWebsite.Extensions;

namespace BlogWebsite.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogDbContext context;

        public PostsController(BlogDbContext context)
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
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            ViewBag.Tags = new MultiSelectList(context.Tags, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Post post, IFormFile Image, int[] tags)
        {
            var path = await FileUploadHelper.UploadAsync(Image);
            post.ImageUrl = path;

            if (ModelState.IsValid)
            {
                post.Date = DateTime.Now;
                context.Posts.Add(post);
                await context.SaveChangesAsync();

                //foreach (var item in tags)
                //{
                //    context.PostTags.Add(new PostTag { TagId = item, PostId = post.Id });
                //}

                //[1, 3, 5] => [{ PostId = 25, TagId = 1 }, { PostId = 25, TagId = 3 }, { PostId = 25, TagId = 5 }]
                context.PostTags.AddRange(tags.Select(x => new PostTag { PostId = post.Id, TagId = x }));
                await context.SaveChangesAsync();

                TempData["Status"] = "New post added!";
                return RedirectToAction("Index");
            }
            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = context.Posts.Find(id);
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            var selectedTagsIds = context.PostTags.Where(x => x.PostId == id).Select(x => x.TagId);
            ViewBag.Tags = new MultiSelectList(context.Tags, "Id", "Name", selectedTagsIds);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Post post, IFormFile Image, int[] tags)
        {
            if (Image != null)
            {
                var path = await FileUploadHelper.UploadAsync(Image);
                post.ImageUrl = path;
            }
            post.Date = DateTime.Now;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
          
            context.UpdateManyToMany(
                context.PostTags.Where(x => x.PostId == post.Id),
                tags.Select(x => new PostTag { PostId = post.Id, TagId = x }),
                x => x.TagId);

            await context.SaveChangesAsync();

            TempData["Status"] = "Post edited!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var post = context.Posts
                .Include(x => x.PostTags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
            return View(post);
        }

        
    }
}
