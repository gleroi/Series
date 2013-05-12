using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Series.Core.Torrents;
using Series.Core.TvShows;

namespace Series.Website.Api
{
    public class LatestController : ApiController
    {
        public Library Library { get; set; }

        public LatestController(Library library)
        {
            this.Library = library;
        }
        
        public IEnumerable<TorrentLink> Get()
        {
            var torrents = Library.Torrents().
                Where(t => t.Status != Status.Ignore).ToList();
            return torrents.OrderByDescending(t => t.CreatedAt);
        }

    }
}