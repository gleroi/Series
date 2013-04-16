using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Core.Torrents;
using Series.Core.TvShows.Providers;

namespace Series.Core.TvShows
{
    public class LibraryBuilder
    {
        public LibraryBuilder(Library library, IMetadataProvider metadata)
        {
            this.Library = library;
            this.MetadataProvider = metadata;
        }

        public Library Library { get; set; }

        public IMetadataProvider MetadataProvider { get; set; }

        public void Add(Serie serie)
        {
            this.MetadataProvider.LoadEpisodes(serie);
            this.Library.Add(serie);
        }

        public IEnumerable<TorrentLink> FindTorrents(Episode ep)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Serie> Search(string name)
        {
            return this.MetadataProvider.GetSeries(name);
        }
    }
}