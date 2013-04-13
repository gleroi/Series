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
        #region Series

        [Fact]
        public void ShouldCollectListOfSeries()
        {
            OmgTorrentCrawler crawler = new OmgTorrentCrawler();
            var urls = crawler.CollectSeriesUrls();

            Assert.NotNull(urls);
            Assert.NotEmpty(urls);
        }

        [Fact]
        public void ShouldCollectSerieTorrents()
        {
            OmgTorrentCrawler crawler = new OmgTorrentCrawler();
            var urls = crawler.CollectSerieTorrents("/series/californication_saison_1_21.html").Result;

            Assert.NotNull(urls);
            Assert.NotEmpty(urls);
        }

        #endregion Series

        #region Seasons

        [Fact]
        private void ShouldCollectListOfSeason()
        {
            OmgTorrentCrawler.SeriePageExtractor extractor = new OmgTorrentCrawler.SeriePageExtractor();
            var urls = extractor.CollectSeasonsUrls(OmgTorrentCrawler.MakeUri("/series/californication_saison_1_21.html"));

            Assert.NotNull(urls);
            Assert.NotEmpty(urls);
            Assert.Equal(6, urls.Count());
        }

        #endregion Seasons

        #region Episodes

        [Fact]
        private void ShouldCollectEpisodes12Torrent()
        {
            OmgTorrentCrawler.SeriePageExtractor extractor = new OmgTorrentCrawler.SeriePageExtractor();
            var urls = extractor.CollectEpisodesUrls(OmgTorrentCrawler.MakeUri("/series/californication_saison_3_21.html"));

            Assert.NotNull(urls);
            Assert.NotEmpty(urls);
            Assert.Equal(12, urls.Count());
        }

        [Fact]
        private void ShouldCollectEpisodesOneTorrent()
        {
            OmgTorrentCrawler.SeriePageExtractor extractor = new OmgTorrentCrawler.SeriePageExtractor();
            var urls = extractor.CollectEpisodesUrls(OmgTorrentCrawler.MakeUri("/series/californication_saison_1_21.html"));

            Assert.NotNull(urls);
            Assert.NotEmpty(urls);
            Assert.Equal(1, urls.Count());
        }

        #endregion Episodes
    }
}