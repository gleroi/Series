using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.TorrentProviders.OmgTorrent;
using Xunit;

namespace Series.TorrentProviders.Tests.OmgTorrent
{
    /// <summary>
    /// Test the Series.TorrentProviders.OmgTorrent.OmgTorrentProvider class
    /// </summary>
    public class OmgTorrentProviderTests
    {
        [Fact]
        public void ShouldFindEpisodeForCalifornication()
        {
            var provider = new OmgTorrentProvider();
            var results = provider.Search("Californication");

            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }
    }
}