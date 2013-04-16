using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.TvShows.Providers
{
    public interface IMetadataProvider
    {
        IEnumerable<Serie> GetSeries(string name);

        void LoadEpisodes(Serie serie);
    }
}