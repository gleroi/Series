using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Series.Website.Api
{

    public class ApiCall
    {
        public string Url { get; set; }
        public string Verb { get; set; }
        public string ReturnType { get; set; }
    }

    public class MetadataController : ApiController
    {
        public IEnumerable<ApiCall> Get()
        {
            var calls = new List<ApiCall>();
            var api = this.Configuration.Services.GetApiExplorer();
            foreach (var desc in api.ApiDescriptions)
            {
                calls.Add(new ApiCall
                {
                    Url = desc.ActionDescriptor.ActionName,
                    Verb = desc.HttpMethod.Method,
                });
            }
            return calls;
        }
    }
}
