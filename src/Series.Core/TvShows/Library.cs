using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Raven.Client;
using Series.Core.Atom;
using Series.Core.Torrents;

namespace Series.Core.TvShows
{
    public class Library
    {
        public Library(IDocumentSession session)
        {
            this.Session = session;
        }

        public string PreferredLanguage { get; set; }

        public IDocumentSession Session { get; set; }

        public void Add(SerieLink serie)
        {
            this.Session.Store(serie);
        }

        public void AddTorrents(IEnumerable<TorrentLink> torrents)
        {
            IEnumerable<TorrentLink> existings = this.Session.Load<TorrentLink>(torrents.Select(t => t.Id)).Where(t => t != null);
            if (existings == null || existings.Count() == 0)
                existings = new List<TorrentLink>();
            TorrentAnalyzer analyzer = new TorrentAnalyzer();
            foreach (var torrent in torrents)
            {
                analyzer.Analyze(torrent);
                var existing = existings.FirstOrDefault(t => t.Id == torrent.Id);
                if (existing != null)
                {
                    existing.Url = torrent.Url;
                    existing.Files = torrent.Files;
                    this.Session.Store(existing);
                }
                else
                {
                    this.Session.Store(torrent);
                }
            }
        }

        public void Commit()
        {
            this.Session.SaveChanges();
        }

        public IQueryable<SerieLink> Series()
        {
            return this.Session.Query<SerieLink>().Customize(ctx => ctx.WaitForNonStaleResults());
        }

        public IQueryable<TorrentLink> Torrents()
        {
            return this.Session.Query<TorrentLink>();
        }
    }
}