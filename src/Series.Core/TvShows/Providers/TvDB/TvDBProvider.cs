using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Core.TvShows.Providers.TvDB;

namespace Series.Core.TvShows.Providers
{
    public class TvDBProvider : IMetadataProvider
    {
        private const string TVDB_ID = "TVDB.Id";

        public TvDBProvider(string apiKey)
        {
            this.ApiKey = apiKey;
            this.Language = "fr";
            this.Client = new TvDBHttpClient(this.ApiKey, this.Language);
        }

        public string Language { get; set; }

        private string ApiKey { get; set; }

        private TvDBHttpClient Client { get; set; }

        public async Task<IEnumerable<Serie>> GetSeries(string name)
        {
            var series = await this.Client.GetSeries(name).ConfigureAwait(false);
            List<Serie> result = new List<Serie>();
            foreach (TvDBSerie s in series)
            {
                var r = new Serie(s.Name);
                r.Description = s.Description;
                r.Metadatas[TVDB_ID] = s.Id;
                result.Add(r);
            }
            return result;
        }

        public async Task LoadEpisodes(Serie serie)
        {
            object value;
            if (serie.Metadatas.TryGetValue(TVDB_ID, out value))
            {
                int id = (int)value;
                TvDBEpisodesResult episodes = await this.Client.GetSerie(id).ConfigureAwait(false);
                foreach (TvDBEpisode ep in episodes.Episodes)
                {
                    Episode result = new Episode()
                    {
                        Title = ep.Name,
                        Season = ep.Season,
                        Opus = ep.Opus
                    };
                    serie.Episodes.Add(result);
                }
            }
            throw new ArgumentException("This serie was not retrieved from the TvDB provider", "serie");
        }
    }
}