using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Series.Core.TvShows.Providers.TvDB
{
    public class TvDBEpisode
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("Language")]
        public string Language { get; set; }

        [XmlElement("EpisodeName")]
        public string Name { get; set; }

        [XmlElement("EpisodeNumber")]
        public int Opus { get; set; }

        [XmlElement("SeasonNumber")]
        public int Season { get; set; }
    }
}