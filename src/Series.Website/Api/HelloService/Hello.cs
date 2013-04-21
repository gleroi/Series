using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace Series.Website.Api.HelloService
{
    [Route("/hello")]
    [Route("/hello/{Name}")]
    public class Hello
    {
        public string Name { get; set; }
    }
}