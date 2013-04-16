using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.TvShows
{
    public class Library
    {
        public Library()
        {
            this._Series = new HashSet<Serie>();
        }

        public string PreferredLanguage { get; set; }

        private ICollection<Serie> _Series { get; set; }

        public void Add(Serie serie)
        {
            this._Series.Add(serie);
        }

        public IQueryable<Serie> Series()
        {
            return this._Series.AsQueryable();
        }
    }
}