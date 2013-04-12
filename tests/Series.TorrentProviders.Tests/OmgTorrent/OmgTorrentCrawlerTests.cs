using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.TorrentProviders.OmgTorrent;
using Xunit;

namespace Series.TorrentProviders.Tests.OmgTorrent
{
    public class OmgTorrentCrawlerTests
    {
        [Fact]
        public void ShouldCollectListOfSeries()
        {
            OmgTorrentCrawler crawler = new OmgTorrentCrawler();
            var urls = crawler.CollectSeriesUrls();

            Assert.NotNull(urls);
            Assert.NotEmpty(urls);
        }

        [Fact]
        private void ShouldCollectListOfSeason()
        {
            OmgTorrentCrawler.SeriePageExtractor extractor = new OmgTorrentCrawler.SeriePageExtractor();
            var urls = extractor.CollectSeasonsUrls(OmgTorrentCrawler.MakeUri("/series/californication_saison_1_21.html"));

            Assert.NotNull(urls);
            Assert.NotEmpty(urls);
        }
    }
}