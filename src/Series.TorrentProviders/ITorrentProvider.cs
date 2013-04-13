using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Core.Torrents;

namespace Series.TorrentProviders
{
    public interface ITorrentProvider
    {
        IEnumerable<Torrent> Search(string term);
    }
}