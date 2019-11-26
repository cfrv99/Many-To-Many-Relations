using AspNetCoreFirstWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFirstWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Data = "Hello, world!";
            ViewData["Name"] = "Gleb";
            return View();
        }

        public IActionResult Experience()
        {
            var profile = new Profile
            {
                Username = "GlebSkripnikov",
                Rating = 5
            };

            return View(profile);
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Data()
        {
            return Json(new { Title = "Title", Content = "Lorem ipsum" });
        }

        public IActionResult Error(int id = 500)
        {
            ViewBag.ErrorId = id;
            return View();
        }
    }
}
