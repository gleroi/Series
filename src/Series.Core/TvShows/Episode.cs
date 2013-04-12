using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.TvShows
{
    /// <summary>
    /// Episode of a serie
    /// </summary>
    public class Episode
    {
        public int Opus { get; set; }

        public int Season { get; set; }

        public int SerieId { get; set; }

        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            Episode ep = obj as Episode;
            if (obj != null)
            {
                return this.Season == ep.Season && this.Opus == ep.Opus;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Season.GetHashCode() ^ Opus.GetHashCode();
        }
    }
}