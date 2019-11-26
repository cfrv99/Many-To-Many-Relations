using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StepWebServer.MyWebHost
{
    delegate void HttpHandler(HttpListenerContext context);

    interface IMiddleware
    {
        public HttpHandler Next { get; set; }

        void Handle(HttpListenerContext context)
        {
            context.Response.Close();
        }
    }
}
