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
        public Serie()
        {
            this.Episodes = new List<Episode>();
        }

        public Serie(string title)
            : this()
        {
            this.Title = title;
        }

        public int Count
        {
            get { return this.Episodes.Count; }
        }

        public string Description { get; set; }

        public List<Episode> Episodes { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }
    }
}