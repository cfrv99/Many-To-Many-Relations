using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace StepWebServer.MyWebHost
{
    class MiddlewareBuilder
    {
        private Stack<Type> middlewares = new Stack<Type>();

        public void Use<T>() where T : IMiddleware
        {
            middlewares.Push(typeof(T));
        }

        public HttpHandler Build()
        {
            var tempMiddlewares = new Stack<Type>(middlewares);
            HttpHandler handler = context => context.Response.Close();
            while (tempMiddlewares.Count != 0)
            {
                var type = tempMiddlewares.Pop();
                //var middleware = Activator.CreateInstance(type) as IMiddleware;
                var middleware = WebHost.Container.GetRequiredService(type) as IMiddleware;
                middleware.Next = handler;
                handler = middleware.Handle;
            }
            return handler;
        }
    }
}
