using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Series.Core.TvShows;
using ServiceStack.ServiceHost;

namespace Series.Website.Api.Series
{
    [Route("/series/library/{LibraryId}")]
    public class Library
    {
        public int LibraryId { get; set; }

        public ICollection<Serie> SeriesToAdd { get; set; }
    }

    public class LibraryResponse
    {
        public int LibraryId { get; set; }

        public ICollection<Serie> Series { get; set; }
    }

    public class LibraryService
    {
        public LibraryResponse Get(Library request)
        {
            LibraryResponse response = new LibraryResponse();

            return response;
        }

        public LibraryResponse Put(Library request)
        {
            LibraryResponse response = new LibraryResponse();

            return response;
        }
    }
}