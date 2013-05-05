using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Series.Core.Torrents;
using Series.Core.TvShows;

namespace Series.Website.Api
{
    public class TorrentsController : ApiController
    {
        public TorrentsController(Library library)
        {
            this.Library = library;
        }

        public Library Library { get; set; }

        public IEnumerable<TorrentLink> Get()
        {
            return Library.Torrents();
        }
    }
}