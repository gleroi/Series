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
            this.CreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get; set; }

        public string Filename { get { return this.Id; } }

        public ICollection<File> Files { get; set; }

        public string Id { get; set; }

        public string Url { get; set; }
    }
}