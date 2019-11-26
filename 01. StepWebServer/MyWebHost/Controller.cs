using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StepWebServer.MyWebHost
{
    abstract class Controller
    {
        public HttpListenerContext Context { get; set; }

        public JsonResult Json(object data)
        {
            var result = new JsonResult(data);
            result.ExecuteResult(Context);
            return result;
        }

        public ViewResult View()
        {
            var result = new ViewResult();
            result.ExecuteResult(Context);
            return result;
        }
    }
}
