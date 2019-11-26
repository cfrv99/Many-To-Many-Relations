using StepWebServer.MyWebHost;
using StepWebServer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StepWebServer.Controllers
{
    class NewsController : Controller
    {
        private readonly LoggerService loggerService;

        public NewsController(LoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public string Latest()
        {
            loggerService.Log("Log from news controller!");
            return "Latest news page";
        }
    }
}
