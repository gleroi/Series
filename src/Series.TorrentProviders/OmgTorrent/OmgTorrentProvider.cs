using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.TorrentProviders.OmgTorrent
{
    public class OmgTorrentProvider : ITorrentProvider
    {
        public IEnumerable<Torrent> Search(string term)
        {
            throw new NotImplementedException();
        }
    }
}