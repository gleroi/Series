using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Series.Core.TvShows;
using Series.Core.TvShows.Providers;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Series.Website.Api.Series
{
    [Route("/series/search/{Term}")]
    public class Search
    {
        public string Term { get; set; }
    }

    public class SearchResponse
    {
        public List<Serie> Series { get; set; }
    }

    public class SearchService : Service
    {
        public SearchService(IMetadataProvider searchProvider)
        {
            this.SearchProvider = searchProvider;
        }

        public IMetadataProvider SearchProvider { get; set; }

        public SearchResponse Get(Search request)
        {
            SearchResponse response = new SearchResponse();
            var task = this.SearchProvider.GetSeries(request.Term);
            response.Series = new List<Serie>(task.Result);
            return response;
        }
    }
}