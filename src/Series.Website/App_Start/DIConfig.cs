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
            builder.RegisterType<Series.TorrentProviders.OmgTorrent.OmgTorrentProvider>();

            builder.RegisterType<Series.Core.TvShows.Library>();
            builder.Register<Raven.Client.IDocumentStore>(context =>
            {
                return MvcApplication.Store;
            }).SingleInstance();

            builder.Register<Raven.Client.IDocumentSession>(context =>
            {
                var store = context.Resolve<Raven.Client.IDocumentStore>();
                return store.OpenSession();
            }).InstancePerHttpRequest();
        }
    }
}