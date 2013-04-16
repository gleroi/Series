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
    public class Serie : WithMetadata, IEnumerable<Episode>
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

        public int Id { get; set; }

        public string Title { get; set; }

        private HashSet<Episode> Episodes { get; set; }

        public void Add(Episode item)
        {
            this.Episodes.Add(item);
        }

        public IEnumerator<Episode> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Remove(Episode item)
        {
            return this.Episodes.Remove(item);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Episodes.GetEnumerator();
        }
    }
}