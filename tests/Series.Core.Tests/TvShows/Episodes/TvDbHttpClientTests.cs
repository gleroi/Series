using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Core.TvShows.Providers.TvDB;
using Xunit;

namespace Series.Core.Tests.TvShows.Episodes
{
    public class TvDbHttpClientTests
    {
        private const string KEY = "6DBF04CA70C63AC8";

        [Fact]
        public void ShouldFindCalifornication()
        {
            TvDBHttpClient tvdb = new TvDBHttpClient(KEY, "fr");
            var series = tvdb.GetSeries("californication").Result;

            Assert.NotNull(series);
            Assert.NotEmpty(series);
            Assert.True(series.All(s => s.Name.ToLowerInvariant().Contains("californication")));
            Assert.True(series.All(s => s.Id != 0));
        }

        [Fact]
        public void ShouldFindEpisodesForCalifornication()
        {
            TvDBHttpClient tvdb = new TvDBHttpClient(KEY, "fr");
            var serie = tvdb.GetSeries("californication").Result.FirstOrDefault();
            var episodes = tvdb.GetSerie(serie.Id).Result;

            Assert.NotNull(episodes);
            Assert.NotNull(episodes.Serie);
            Assert.NotNull(episodes.Episodes);
            Assert.NotEmpty(episodes.Episodes);
        }
    }
}