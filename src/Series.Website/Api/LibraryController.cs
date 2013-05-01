using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using Series.Core.TvShows;

namespace Series.Website
{
    public class LibraryController : ApiController
    {
        public LibraryController(Library library)
        {
            this.Library = library;
        }

        private Library Library { get; set; }

        // GET api/<controller>
        public LibraryResponse Get()
        {
            LibraryResponse response = new LibraryResponse();

            response.Series = Library.All().ToList();
            return response;
        }

        // POST api/<controller>
        public LibraryResponse Post([FromBody]LibraryRequest request)
        {
            LibraryResponse response = new LibraryResponse();
            response.LibraryId = request.LibraryId;
            if (request.Series != null)
            {
                foreach (var serie in request.Series)
                {
                    Library.Add(serie);
                }
                this.Library.Commit();
            }
            response.Series = Library.All().ToList();
            return response;
        }
    }

    public class LibraryRequest
    {
        public List<Serie> Series;

        public int LibraryId { get; set; }
    }

    public class LibraryResponse
    {
        public int LibraryId { get; set; }

        public List<Serie> Series { get; set; }
    }
}