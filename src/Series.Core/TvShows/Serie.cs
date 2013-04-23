using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.TvShows
{
    /// <summary>
    /// Tv show
    /// </summary>
    public class Serie : WithMetadata
    {
        public Serie(string title)
            : base()
        {
            this.Episodes = new HashSet<Episode>();
            this.Title = title;
        }

        public int Count
        {
            get { return this.Episodes.Count; }
        }

        public HashSet<Episode> Episodes { get; private set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}