using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Series.Core.Atom;
using Series.Core.TvShows;
using Series.Core.TvShows.Providers;
using Series.TorrentProviders.OmgTorrent;

namespace Series.Website.Api
{
    [Authorize]
    public class SearchController : ApiController
    {
        public SearchController(OmgTorrentProvider searchProvider)
        {
            this.SearchProvider = searchProvider;
        }

        public OmgTorrentProvider SearchProvider { get; set; }

        public SearchResponse Get([FromUri] SearchRequest request)
        {
            SearchResponse response = new SearchResponse();
            var series = this.SearchProvider.AllSeries();
            if (request != null & !String.IsNullOrWhiteSpace(request.Term))
                series = series.Where(s => s.Title.Contains(request.Term));
            response.Series = series.ToList();
            return response;
        }
    }

    public class SearchRequest
    {
        public string Term { get; set; }
    }

    public class SearchResponse
    {
        public List<SerieLink> Series { get; set; }
    }
}