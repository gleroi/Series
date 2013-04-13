using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Core.Torrents;
using Xunit;

namespace Series.Core.Tests.Torrents
{
    /// <summary>
    /// Test the class Series.Core.Torrents.TorrentAnalyzer
    /// </summary>
    public class TorrentAnalyzerTests
    {
        [Fact]
        public void ShouldRetrieveFileInfos()
        {
            TorrentLink link = new TorrentLink
            {
                Url = "http://www.omgtorrent.com/torrents/Series/californication/californication_saison_1.torrent"
            };
            TorrentAnalyzer analyzer = new TorrentAnalyzer();
            analyzer.Analyze(link);

            Assert.NotNull(link.Files);
            Assert.NotEmpty(link.Files);
            Assert.True(link.Files.All(f => f.Episode != null));
        }
    }
}