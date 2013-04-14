using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.TvShows.Providers
{
    public class TvDBProvider : IMetadataProvider
    {
        public TvDBProvider(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        private string ApiKey { get; set; }

        public IEnumerable<Episode> GetEpisodes(Serie serie)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Serie> GetSeries()
        {
            throw new NotImplementedException();
        }
    }
}