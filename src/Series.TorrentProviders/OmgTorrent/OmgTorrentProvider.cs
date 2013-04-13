using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Core.Torrents;

namespace Series.TorrentProviders.OmgTorrent
{
    /// <summary>
    /// Search torrents on OmgTorrent tv shows section.
    /// </summary>
    public class OmgTorrentProvider : ITorrentProvider
    {
        /// <summary>
        /// Search torrents related to a serie containing <paramref name="term"/>
        /// </summary>
        /// <param name="term">a term in the serie title</param>
        /// <returns></returns>
        public IEnumerable<Torrent> Search(string term)
        {
            string searchTerm = term.ToLowerInvariant();
            OmgTorrentCrawler crawler = new OmgTorrentCrawler();
            var series = crawler.CollectSeriesUrls()
                .Where(url => url.Contains(searchTerm));

            List<Torrent> torrents = new List<Torrent>();
            foreach (string url in series)
            {
                var results = crawler.CollectSerieTorrents(url).Result;
                if (results != null)
                    torrents.AddRange(results);
            }
            return torrents;
        }
    }
}