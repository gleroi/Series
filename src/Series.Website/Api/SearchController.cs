using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Series.Core.TvShows;
using Series.Core.TvShows.Providers;

namespace Series.Website.Api
{
    public class SearchController : ApiController
    {
        public SearchController(IMetadataProvider searchProvider)
        {
            this.SearchProvider = searchProvider;
        }

        public IMetadataProvider SearchProvider { get; set; }

        public SearchResponse Get([FromUri] SearchRequest request)
        {
            SearchResponse response = new SearchResponse();
            var task = this.SearchProvider.GetSeries(request.Term);
            response.Series = new List<Serie>(task.Result);
            return response;
        }
    }

    public class SearchRequest
    {
        public string Term { get; set; }
    }

    public class SearchResponse
    {
        public List<Serie> Series { get; set; }
    }
}