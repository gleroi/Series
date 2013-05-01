using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Raven.Client.Embedded;

namespace Series.Website
{
    public static class DIConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            RegisterTypes(builder);

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<Series.Core.TvShows.Providers.TvDBProvider>()
                .As<Series.Core.TvShows.Providers.IMetadataProvider>()
                .WithParameter(new TypedParameter(typeof(string), MvcApplication.TVDB_API_KEY));

            builder.RegisterType<Series.Core.TvShows.Library>();
            builder.Register<Raven.Client.IDocumentStore>(context =>
            {
                var store = new EmbeddableDocumentStore()
                {
                    DataDirectory = HttpContext.Current.Server.MapPath("~/App_Data")
                };
                store.Initialize();
                return store;
            }).SingleInstance();

            builder.Register<Raven.Client.IDocumentSession>(context =>
            {
                var store = context.Resolve<Raven.Client.IDocumentStore>();
                return store.OpenSession();
            }).InstancePerHttpRequest();
        }
    }
}