using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace StepWebServer.MyWebHost
{
    class MVCMiddleware : IMiddleware
    {
        public HttpHandler Next { get; set; }

        public void Handle(HttpListenerContext context)
        {
            // /Home/Index
            var parts = context.Request.RawUrl.Split('/', StringSplitOptions.RemoveEmptyEntries); //["Home", "Index"]

            var controllerName = "StepWebServer.Controllers." + parts[0] + "Controller";
            var methodName = parts[1].Split('?')[0];

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Type controllerType = currentAssembly.GetType(controllerName);
            if (controllerType != null)
            {
                //Controller controllerObj = Activator.CreateInstance(controllerType) as Controller;
                Controller controllerObj = WebHost.Container.GetRequiredService(controllerType) as Controller;
                controllerObj.Context = context;
                MethodInfo controllerMethod = controllerType.GetMethod(methodName);
                if (controllerMethod != null)
                {
                    ParameterInfo[] parameters = controllerMethod.GetParameters();

                    object[] paramValues = new object[parameters.Length];
                    int index = 0;
                    foreach (var item in parameters)
                    {
                        paramValues[index++] = Convert.ChangeType(context.Request.QueryString[item.Name], item.ParameterType);
                    }

                    controllerMethod.Invoke(controllerObj, paramValues);
                }
            }

            //Console.WriteLine($"Controller: {controllerName}");
            //Console.WriteLine($"Method: {methodName}");

            Next?.Invoke(context);
        }
    }
}
