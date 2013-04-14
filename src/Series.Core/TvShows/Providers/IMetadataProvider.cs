using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.TvShows.Providers
{
    public interface IMetadataProvider
    {
        IEnumerable<Episode> GetEpisodes(Serie serie);

        IEnumerable<Serie> GetSeries();
    }
}