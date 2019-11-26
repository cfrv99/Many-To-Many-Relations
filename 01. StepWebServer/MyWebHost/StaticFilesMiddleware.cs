using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace StepWebServer.MyWebHost
{
    class StaticFilesMiddleware : IMiddleware
    {
        public HttpHandler Next { get; set; }

        public void Handle(HttpListenerContext context)
        {
            if (Path.HasExtension(context.Request.RawUrl))
            {
                var filename = context.Request.RawUrl.Substring(1);
                try
                {
                    var bytes = File.ReadAllBytes($"wwwroot/{filename}");

                    if (Path.GetExtension(filename) == ".html")
                        context.Response.AddHeader("Content-Type", "text/html");

                    else if (Path.GetExtension(filename) == ".jpg")
                        context.Response.AddHeader("Content-Type", "image/jpg");

                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 404;
                    context.Response.StatusDescription = "File not found!";
                }
                context.Response.Close();
            }
            else
            {
                Next?.Invoke(context);
            }
        }
    }
}
