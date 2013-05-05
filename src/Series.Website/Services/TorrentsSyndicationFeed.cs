using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Series.Core.Torrents;
using Series.Core.TvShows;

namespace Series.Website.Services
{
    public class TorrentsSyndicationFeed
    {
        public TorrentsSyndicationFeed(IEnumerable<TorrentLink> torrents)
        {
            this.Torrents = torrents;
        }

        public IEnumerable<TorrentLink> Torrents { get; set; }

        public SyndicationFeed AsFeed()
        {
            var feed = new SyndicationFeed("Series", "Series aggregation", null);
            feed.Authors.Add(new SyndicationPerson("leroi.g@gmail.com", "Guillaume Leroi", null));
            feed.Items = BuildItems().ToList();

            return feed;
        }

        private TextSyndicationContent BuildContent(TorrentLink link)
        {
            StringBuilder sb = new StringBuilder();
            foreach (File f in link.Files)
            {
                var episode = f.Episode ?? new Episode();
                sb.AppendLine(String.Format("{0} (s{1:00}e{2:00})", f.Filename, episode.Season, episode.Opus));
            }
            return new TextSyndicationContent(sb.ToString(), TextSyndicationContentKind.Plaintext);
        }

        private IEnumerable<SyndicationItem> BuildItems()
        {
            foreach (TorrentLink link in this.Torrents)
            {
                yield return BuildItems(link);
            }
        }

        private SyndicationItem BuildItems(TorrentLink link)
        {
            var item = new SyndicationItem
            {
                Title = new TextSyndicationContent(link.Filename, TextSyndicationContentKind.Plaintext),
                Summary = BuildContent(link),
                PublishDate = new DateTimeOffset(link.CreatedAt),
                Id = link.Id
            };
            var uri = new Uri(link.Url);
            item.ElementExtensions.Add(BuildLink(link.Url));
            return item;
        }

        private XElement BuildLink(string url)
        {
            return new XElement("enclosure", new XAttribute("url", url), new XAttribute("type", "application/x-bittorrent"));
        }
    }
}