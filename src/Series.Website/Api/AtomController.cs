using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Web.Http;
using Series.Core.Torrents;
using Series.Core.TvShows;
using Series.Website.Api.Formatters;
using Series.Website.Services;

namespace Series.Website.Api
{
    [AllowAnonymous]
    public class AtomController : ApiController
    {
        public AtomController(Library library)
        {
            this.Library = library;
        }

        public Library Library { get; set; }

        public HttpResponseMessage Get()
        {
            var torrents = this.Library.Torrents()
                .Where(t => t.Status != Status.Ignore);
            TorrentsSyndicationFeed feed = new TorrentsSyndicationFeed(torrents);
            return Request.CreateResponse(HttpStatusCode.OK, feed.AsFeed(), new FeedMediaTypeFormatter(), "application/xml");
        }
    }
}