using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StepWebServer.MyWebHost
{
    class WebHost
    {
        public WebHost(int port)
        {
            this.port = port;
        }

        public void UseStartup<T>() where T : IStartup, new()
        {
            IStartup startup = new T();

            startup.Configure(middlewareBuilder);

            ServiceCollection collection = new ServiceCollection();
            startup.ConfigureServices(collection);
            WebHost.Container = collection.BuildServiceProvider();
        }

        public void Run()
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add($"http://localhost:{port}/");
            httpListener.Prefixes.Add($"http://10.2.25.56:{port}/");
            httpListener.Start();

            Console.WriteLine($"Server started on port {port}");

            while (true)
            {
                HttpListenerContext context = httpListener.GetContext();
                Task.Run(() =>
                {
                    HandleRequest(context);
                });
            }
        }

        public void HandleRequest(HttpListenerContext context)
        {
            handler = middlewareBuilder.Build();
            handler?.Invoke(context);
        }

        public static ServiceProvider Container;
        private HttpHandler handler;
        private int port;
        private HttpListener httpListener;
        private MiddlewareBuilder middlewareBuilder = new MiddlewareBuilder();
    }
}
