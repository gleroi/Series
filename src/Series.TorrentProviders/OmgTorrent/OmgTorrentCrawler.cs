using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;
using Series.Core.Atom;
using Series.Core.Torrents;

namespace Series.TorrentProviders.OmgTorrent
{
    public class OmgTorrentCrawler
    {
        private static Uri SERIE_ROOT = new Uri("http://www.omgtorrent.com/series/");

        public static Uri MakeUri(string url)
        {
            return new Uri(SERIE_ROOT, url);
        }

        public IEnumerable<SerieLink> CollectSeriesUrls()
        {
            HtmlDocument reader = GetPage(SERIE_ROOT);
            List<SerieLink> urls = new List<SerieLink>();
            var nodes = reader.DocumentNode.SelectNodes("//select[@name='listeurl']");
            foreach (HtmlNode select in nodes)
            {
                foreach (HtmlNode option in select.Elements("option"))
                {
                    string url = option.Attributes["value"].Value;
                    if (!String.IsNullOrWhiteSpace(url))
                    {
                        urls.Add(DecodeElement(option));
                    }
                }
            }
            return urls;
        }

        public async Task<IEnumerable<TorrentLink>> CollectSerieTorrents(string url)
        {
            List<TorrentLink> torrents = new List<TorrentLink>();
            SeriePageExtractor extractor = new SeriePageExtractor();

            var episodes = extractor.CollectSeasonsUrls(MakeUri(url))
                .SelectMany(season => extractor.CollectEpisodesUrls(MakeUri(season)));
            using (HttpClient client = new HttpClient())
            {
                foreach (string episode in episodes)
                {
                    var response = await client.GetAsync(MakeUri(episode)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        torrents.Add(new TorrentLink
                        {
                            Url = response.RequestMessage.RequestUri.AbsoluteUri,
                            Id = response.RequestMessage.RequestUri.Segments.Last()
                        });
                    }
                }
            }

            return torrents;
        }

        private static HtmlDocument GetPage(Uri url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] data = client.DownloadData(url);
                    using (MemoryStream stream = new MemoryStream(data))
                    {
                        HtmlDocument reader = new HtmlDocument();
                        reader.Load(stream);
                        return reader;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
            }
            return null;
        }

        private SerieLink DecodeElement(HtmlNode option)
        {
            var serie = new SerieLink();
            string url = option.Attributes["value"].Value;
            serie.Url = MakeUri(url).AbsoluteUri;
            serie.Title = String.IsNullOrWhiteSpace(option.InnerText) ? url : option.InnerText;
            string id = url.Split('_').Last().Split('.').First();
            serie.Id = "OMG_" + id;
            return serie;
        }

        public class SeriePageExtractor
        {
            public IEnumerable<string> CollectEpisodesUrls(Uri page)
            {
                List<string> urls = new List<string>();
                string pattern = "/clic_dl.php?";
                HtmlDocument doc = GetPage(page);
                var links = doc.DocumentNode.SelectNodes("//a")
                    .Select(node => WebUtility.HtmlDecode(node.Attributes["href"].Value))
                    .Where(url => url != null && url.StartsWith(pattern))
                    .ToList();

                urls.AddRange(links);
                return urls;
            }

            public IEnumerable<string> CollectSeasonsUrls(Uri page)
            {
                List<string> urls = new List<string>();

                Regex rexp = new Regex(@"^/series/([a-z-A-Z0-9]+)_saison_\d+_\d+\.html$");
                string pattern = rexp.Replace(page.AbsolutePath, "/series/$1");
                rexp = null;
                HtmlDocument doc = GetPage(page);
                var links = doc.DocumentNode.SelectNodes("//a")
                    .Select(node => WebUtility.UrlDecode(node.Attributes["href"].Value))
                    .Where(url => url != null && url.StartsWith(pattern))
                    .ToList();
                urls.AddRange(links);
                return urls;
            }
        }
    }
}