using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StepWebServer.MyWebHost
{
    interface IActionResult
    {
        void ExecuteResult(HttpListenerContext context);
    }
}
