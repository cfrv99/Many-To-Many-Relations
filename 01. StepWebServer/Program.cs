using StepWebServer.MyWebHost;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StepWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHost(80);
            host.UseStartup<Startup>();
            host.Run();
        }
    }

    /*  
     *  DI
     *  appsettings.json
     *  Logging
     *  IActionResult
     *  Repository
     *  return View()
     *  return Json()
     *  Dev and Prod Errors
     */
}
