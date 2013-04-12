using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.TvShows
{
    public class Library
    {
        private ICollection<Episode> _episodes { get; set; }

        private ICollection<Serie> _series { get; set; }

        public IQueryable<Episode> Episodes()
        {
            return _episodes.AsQueryable();
        }

        public IQueryable<Episode> Episodes(Serie serie)
        {
            return _episodes.Where(e => e.SerieId == serie.Id).AsQueryable();
        }

        public IQueryable<Serie> Series()
        {
            return _series.AsQueryable();
        }
    }
}