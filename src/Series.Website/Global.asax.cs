using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Quartz;
using Quartz.Impl;
using Raven.Client;
using Raven.Client.Embedded;

namespace Series.Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public const string TVDB_API_KEY = "6DBF04CA70C63AC8";

        public static IDocumentStore Store
        {
            get;
            private set;
        }

        private IScheduler Scheduler { get; set; }

        private ISchedulerFactory SchedulerFactory { get; set; }

        protected void Application_Start()
        {
            // data store
            InitStore();

            // mvc application
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DIConfig.Register(GlobalConfiguration.Configuration);

            // scheduler
            StartScheduler();
            ScheduleJobs();
        }

        private void InitStore()
        {
            Store = new EmbeddableDocumentStore
            {
                DataDirectory = this.Server.MapPath("~/App_Data")
            };
            Store.Initialize();
        }

        private void ScheduleJobs()
        {
            // construct job info
            IJobDetail jobDetail = new JobDetailImpl("torrentLoader", null, typeof(Series.Website.Services.TorrentsLoaderJob));

            // construct trigger
            var builder = TriggerBuilder.Create();
            builder
                .StartNow()
                .WithCalendarIntervalSchedule(ctx => ctx.WithIntervalInMinutes(5))
                .WithDescription("Torrent loader trigger");
            ITrigger trigger = builder.Build();

            // schedule
            this.Scheduler.ScheduleJob(jobDetail, trigger);
        }

        private void StartScheduler()
        {
            this.SchedulerFactory = new StdSchedulerFactory();
            this.Scheduler = this.SchedulerFactory.GetScheduler();
            this.Scheduler.Start();
        }
    }
}