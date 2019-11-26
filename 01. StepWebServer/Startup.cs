using Microsoft.Extensions.DependencyInjection;
using StepWebServer.Controllers;
using StepWebServer.MyWebHost;
using StepWebServer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StepWebServer
{
    class Startup : IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<LoggerService>();

            services.AddTransient<HomeController>();
            services.AddTransient<NewsController>();

            services.AddTransient<LoggerMiddleware>();
            services.AddTransient<MVCMiddleware>();
            services.AddTransient<StaticFilesMiddleware>();
        }

        public void Configure(MiddlewareBuilder builder)
        {
            builder.Use<LoggerMiddleware>();
            builder.Use<StaticFilesMiddleware>();
            builder.Use<MVCMiddleware>();
        }
    }
}
