using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Series.Core.TvShows.Providers.TvDB
{
    [XmlRoot("Data")]
    public class TvDBEpisodesResult
    {
        [XmlElement("Episode")]
        public List<TvDBEpisode> Episodes { get; set; }

        [XmlElement("Series")]
        public TvDBSerie Serie { get; set; }
    }
}