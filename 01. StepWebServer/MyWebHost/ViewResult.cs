using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace StepWebServer.MyWebHost
{
    class ViewResult : IActionResult
    {
        public void ExecuteResult(HttpListenerContext context)
        {
            var parts = context.Request.RawUrl.Split('/', StringSplitOptions.RemoveEmptyEntries); //["Home", "Index"]
            var controllerName = parts[0];
            var methodName = parts[1];
            var path = $"Views/{controllerName}/{methodName}.html";
            var bytes = File.ReadAllBytes(path);
            context.Response.ContentType = "text/html";
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
        }
    }
}
