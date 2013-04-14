using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Series.Core.TvShows.Providers.TvDB
{
    public class TvDBSerie
    {
        [XmlElement("Overview")]
        public string Description;

        [XmlElement("seriesid")]
        public int Id;

        [XmlElement("IMDB_ID")]
        public string ImdbId;

        [XmlElement("SeriesName")]
        public string Name;

        [XmlElement("banner")]
        public string BannerUrl { get; set; }

        public override string ToString()
        {
            return Name + " (" + ImdbId + ", " + Id + ")\n" + Description;
        }
    }
}