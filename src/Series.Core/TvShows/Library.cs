using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Raven.Client;

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

        public void Add(Serie serie)
        {
            this.Session.Store(serie);
        }

        public IQueryable<Serie> All()
        {
            return this.Session.Query<Serie>().Customize(ctx => ctx.WaitForNonStaleResults());
        }

        public void Commit()
        {
            this.Session.SaveChanges();
        }
    }
}