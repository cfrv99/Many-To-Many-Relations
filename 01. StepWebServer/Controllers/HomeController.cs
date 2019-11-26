using StepWebServer.MyWebHost;
using StepWebServer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StepWebServer.Controllers
{
    class HomeController : Controller
    {
        private readonly LoggerService loggerService;

        public HomeController(LoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public IActionResult Index()
        {
            loggerService.Log("Logger works!");
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Data(string name, int age)
        {
            return Json(new { Name = name ?? "Gleb", Age = age });
        }
    }
}
