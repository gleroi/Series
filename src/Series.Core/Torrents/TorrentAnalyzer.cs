using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MonoTorrent.Common;

namespace Series.Core.Torrents
{
    /// <summary>
    /// Analyses a torrent link and provides informations about it
    /// </summary>
    public class TorrentAnalyzer
    {
        /// <summary>
        /// Analyses and retrieve informations of a torrent
        /// </summary>
        /// <param name="torrent"></param>
        public void Analyze(TorrentLink link)
        {
            Torrent torrent = GetTorrent(link);
            foreach (TorrentFile file in torrent.Files)
            {
                link.Files.Add(new File
                {
                    Filename = file.FullPath
                });
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