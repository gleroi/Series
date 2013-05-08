using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using Series.Core.Atom;
using Series.Core.TvShows;
using Series.Core.TvShows.Providers;

namespace Series.Website
{
    [Authorize]
    public class LibraryController : ApiController
    {
        public LibraryController(Library library)
        {
            this.Library = library;
        }

        public IMetadataProvider MetadataProvider { get; set; }

        private Library Library { get; set; }

        // GET api/<controller>
        public LibraryResponse Get()
        {
            LibraryResponse response = new LibraryResponse();

            response.Series = Library.Series().ToList();
            return response;
        }

        // POST api/<controller>
        public LibraryResponse Post([FromBody]LibraryRequest request)
        {
            LibraryResponse response = new LibraryResponse();
            if (request.Series != null)
            {
                foreach (var serie in request.Series)
                {
                    Library.Add(serie);
                }
                this.Library.Commit();
            }
            response.Series = Library.Series().ToList();
            return response;
        }
    }

    public class LibraryRequest
    {
        public List<SerieLink> Series;
    }

    public class LibraryResponse
    {
        public List<SerieLink> Series { get; set; }
    }
}