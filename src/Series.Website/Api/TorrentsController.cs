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

        public TorrentResponse Get()
        {
            var response = new TorrentResponse();
            var torrents = Library.Torrents().ToList()
                .GroupBy(t => t.SerieLinkId);
            var series = Library.Series(torrents.Select(g => g.Key).ToArray());
            foreach (var group in torrents)
            {
                var groupItem = new TorrentResponse.SerieItem();
                groupItem.Torrents = group.OrderBy(t => t.CreatedAt).ToList();
                groupItem.Id = group.Key;
                var serie = series.FirstOrDefault(s => s.Id == group.Key);
                if (serie != null)
                    groupItem.Title = serie.Title ?? serie.Url;
                else
                    groupItem.Title = group.Key;
                response.Series.Add(groupItem);
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