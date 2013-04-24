using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Series.Core.TvShows;
using ServiceStack.ServiceHost;

namespace Series.Website.Api.Series
{
    [Route("/series/library")]
    [Route("/series/library/{LibraryId}")]
    public class LibraryRequest
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
        public LibraryService()
        {
            this.Library = new Library();
        }

        private Library Library { get; set; }

        public LibraryResponse Get(LibraryRequest request)
        {
            LibraryResponse response = new LibraryResponse();
            response.Series = Library.Series().ToList();
            return response;
        }

        public LibraryResponse Put(LibraryRequest request)
        {
            LibraryResponse response = new LibraryResponse();
            foreach (var serie in request.SeriesToAdd)
            {
                Library.Add(serie);
            }
            response.Series = Library.Series().ToList();
            return response;
        }
    }
}