using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Series.Core.Torrents;
using Series.Core.TvShows;

namespace Series.Website.Api
{
    public class TorrentResponse
    {
        public TorrentResponse()
        {
            this.Series = new List<SerieItem>();
        }

        public List<SerieItem> Series { get; set; }

        public class SerieItem
        {
            public string Id { get; set; }

            public string Title { get; set; }

            public IEnumerable<TorrentLink> Torrents { get; set; }
        }
    }

    public class UpdateStatusRequest
    {
        public string Id { get; set; }
        public Status Status { get; set; }
    }

    [Authorize]
    public class TorrentsController : ApiController
    {
        public TorrentsController(Library library)
        {
            this.Library = library;
        }

        public Library Library { get; set; }

        public TorrentResponse Get(string serieId)
        {
            var response = new TorrentResponse();
            var serie = Library.Series(serieId).FirstOrDefault();
            if (serie != null)
            {
                var torrents = Library.Torrents(serie).OrderBy(t => t.CreatedAt).ToList();
                var groupItem = new TorrentResponse.SerieItem();
                groupItem.Torrents = torrents;
                groupItem.Id = serie.Id;
                groupItem.Title = serie.Title ?? serie.Url;
                response.Series.Add(groupItem);
                return response;
            }
            return response;
        }

        public HttpResponseMessage Put([FromBody]UpdateStatusRequest request)
        {
            try
            {
                var torrent = Library.Torrents(request.Id).FirstOrDefault();
                if (torrent != null)
                {
                    torrent.Status = request.Status;
                    Library.Commit();
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "torrent " + request.Id + " not found");
                }
            }
            catch (Exception ex) {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }

}