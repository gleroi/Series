using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Series.Core.TvShows.Providers.TvDB
{
    public class TvDBHttpClient : HttpClient
    {
        private const string API_SERIE_BY_ID = "http://www.thetvdb.com/api/{0}/series/{1}/all/{2}.xml";
        private const string API_SERIE_BY_NAME = "http://www.thetvdb.com/api/GetSeries.php?language={0}&seriesname={1}";

        public TvDBHttpClient(string apiKey, string language)
        {
            this.ApiKey = apiKey;
            this.Language = language;
        }

        private string ApiKey { get; set; }

        private string Language { get; set; }

        public async Task<TvDBEpisodesResult> GetSerie(int id)
        {
            string url = String.Format(API_SERIE_BY_ID, this.ApiKey, id, this.Language);
            var response = await GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TvDBEpisodesResult));
                TvDBEpisodesResult result = serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as TvDBEpisodesResult;
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<TvDBSerie>> GetSeries(string showName)
        {
            string url = String.Format(API_SERIE_BY_NAME, this.Language, showName);
            List<TvDBSerie> series = new List<TvDBSerie>();

            var response = await GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TvDBSeriesResult));
                TvDBSeriesResult result = serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as TvDBSeriesResult;
                if (result != null)
                {
                    return result.Series;
                }
            }
            return series;
        }
    }
}