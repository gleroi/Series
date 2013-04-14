using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Series.Core.TvShows.Providers.TvDB
{
    [XmlRoot("Data")]
    public class TvDBSeriesResult
    {
        [XmlElement("Series")]
        public List<TvDBSerie> Series { get; set; }
    }
}