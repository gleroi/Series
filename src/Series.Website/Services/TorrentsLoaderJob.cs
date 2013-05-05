using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quartz;
using Raven.Client;
using Raven.Client.Embedded;
using Series.Core.Atom;
using Series.Core.TvShows;
using Series.TorrentProviders.OmgTorrent;

namespace Series.Website.Services
{
    public class TorrentsLoaderJob : IJob
    {
        public Library Library { get; set; }

        public IDocumentSession Session { get; set; }

        public IDocumentStore Store { get; set; }

        public OmgTorrentProvider TorrentProvider { get; set; }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                System.Diagnostics.Trace.TraceInformation("TorrentsLoaderJob : starting at {0}", DateTime.Now);
                Init(context.JobDetail.JobDataMap);

                foreach (SerieLink serie in Library.Series())
                {
                    var torrents = TorrentProvider.GetTorrents(serie);
                    this.Library.AddTorrents(torrents);
                }
                this.Library.Commit();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw new JobExecutionException(ex);
            }
            finally
            {
                if (this.Session != null)
                    this.Session.Dispose();
                System.Diagnostics.Trace.TraceInformation("TorrentsLoaderJob : ending at {0}", DateTime.Now);
            }
        }

        private void Init(JobDataMap jobDataMap)
        {
            this.Store = MvcApplication.Store;
            this.Session = this.Store.OpenSession();
            this.Library = new Library(this.Session);
            this.TorrentProvider = new OmgTorrentProvider();
        }
    }
}