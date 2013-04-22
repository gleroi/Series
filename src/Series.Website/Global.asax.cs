using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ServiceStack.WebHost.Endpoints;

namespace Series.Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            new ServicesAppHost().Init();
        }

        #region Services Stack

        private const string TVDB_API_KEY = "6DBF04CA70C63AC8";

        public class ServicesAppHost : AppHostBase
        {
            public ServicesAppHost()
                : base("Series services stack", typeof(Series.Website.Api.HelloService.HelloService).Assembly)
            { }

            public override void Configure(Funq.Container container)
            {
                container.Register<Series.Core.TvShows.Providers.IMetadataProvider>(ct => new Series.Core.TvShows.Providers.TvDBProvider(TVDB_API_KEY));
            }
        }

        #endregion Services Stack
    }
}