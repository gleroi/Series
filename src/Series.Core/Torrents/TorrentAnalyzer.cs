using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MonoTorrent.Common;
using Series.Core.TvShows;
using Series.Core.TvShows.Episodes;

namespace Series.Core.Torrents
{
    /// <summary>
    /// Analyses a torrent link and provides informations about it
    /// </summary>
    public class TorrentAnalyzer
    {
        public TorrentAnalyzer()
        {
            this.Extractor = new FilenameInfosExtractor();
        }

        public FilenameInfosExtractor Extractor { get; set; }

        /// <summary>
        /// Analyses and retrieve informations of a torrent
        /// </summary>
        /// <param name="torrent"></param>
        public void Analyze(TorrentLink link)
        {
            Torrent torrent = this.GetTorrent(link);
            foreach (TorrentFile file in torrent.Files)
            {
                var f = new File
                {
                    Filename = file.FullPath
                };

                Episode ep = Extractor.Extract(f.Filename);
                if (ep != null)
                    f.Episode = ep;

                link.Files.Add(f);
            }
        }

        private Torrent GetTorrent(TorrentLink link)
        {
            using (HttpClient client = new HttpClient())
            {
                var data = client.GetByteArrayAsync(link.Url).Result;
                Torrent torrent = Torrent.Load(data);
                return torrent;
            }
        }
    }
}