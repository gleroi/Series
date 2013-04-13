using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.Torrents
{
    public class TorrentLink
    {
        public TorrentLink()
        {
            this.Files = new HashSet<File>();
        }

        public string Filename { get; set; }

        public ICollection<File> Files { get; set; }

        public string Url { get; set; }
    }
}