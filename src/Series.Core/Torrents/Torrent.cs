using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.Torrents
{
    public class Torrent
    {
        public Torrent()
        {
            this.Files = new HashSet<File>();
        }

        public string Filename { get; set; }

        public string Url { get; set; }

        private ICollection<File> Files { get; set; }
    }
}