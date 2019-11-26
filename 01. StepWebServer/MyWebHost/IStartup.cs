using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace StepWebServer.MyWebHost
{
    interface IStartup
    {
        public void ConfigureServices(IServiceCollection services);
        public void Configure(MiddlewareBuilder builder);
    }
}
