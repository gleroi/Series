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
            this.Title = title;
        }

        public int Id { get; set; }

        public string Title { get; set; }
    }
}