using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.TvShows.Providers
{
    public interface IMetadataProvider
    {
        Task<IEnumerable<Serie>> GetSeries(string name);

        Task LoadEpisodes(Serie serie);
    }
}