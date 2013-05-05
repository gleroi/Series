using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Core.Atom;
using Series.Core.Torrents;

namespace Series.TorrentProviders.OmgTorrent
{
    /// <summary>
    /// Search torrents on OmgTorrent tv shows section.
    /// </summary>
    public class OmgTorrentProvider : ITorrentProvider
    {
        public IEnumerable<SerieLink> AllSeries()
        {
            OmgTorrentCrawler crawler = new OmgTorrentCrawler();
            var series = crawler.CollectSeriesUrls();
            return series;
        }

        public IEnumerable<TorrentLink> GetTorrents(SerieLink serie)
        {
            List<TorrentLink> torrents = new List<TorrentLink>();
            OmgTorrentCrawler crawler = new OmgTorrentCrawler();
            var task = crawler.CollectSerieTorrents(serie.Url);
            task.ConfigureAwait(false);
            var results = task.Result;
            if (results != null)
                torrents.AddRange(results);
            return torrents;
        }

        /// <summary>
        /// Search torrents related to a serie containing <paramref name="term"/>
        /// </summary>
        /// <param name="term">a term in the serie title</param>
        /// <returns></returns>
        public IEnumerable<TorrentLink> Search(string term)
        {
            string searchTerm = term.ToLowerInvariant();
            OmgTorrentCrawler crawler = new OmgTorrentCrawler();
            var series = crawler.CollectSeriesUrls()
                .Where(s => s.Title.Contains(searchTerm));

            List<TorrentLink> torrents = new List<TorrentLink>();
            foreach (SerieLink s in series)
            {
                var task = crawler.CollectSerieTorrents(s.Url);
                task.ConfigureAwait(false);
                var results = task.Result;
                if (results != null)
                    torrents.AddRange(results);
            }
            return torrents;
        }
    }
}