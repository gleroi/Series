using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface;

namespace Series.Website.Api.HelloService
{
    public class HelloService : Service
    {
        public HelloResponse Any(Hello request)
        {
            return new HelloResponse() { Response = "Hello " + request.Name };
        }
    }
}