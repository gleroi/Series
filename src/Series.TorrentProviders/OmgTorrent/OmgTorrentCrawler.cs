using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace Series.TorrentProviders.OmgTorrent
{
    public class OmgTorrentCrawler
    {
        private static Uri SERIE_ROOT = new Uri("http://www.omgtorrent.com/series/");

        public static Uri MakeUri(string url)
        {
            return new Uri(SERIE_ROOT, url);
        }

        public IEnumerable<string> CollectSeriesUrls()
        {
            HtmlDocument reader = GetPage(SERIE_ROOT);
            List<string> urls = new List<string>();
            var nodes = reader.DocumentNode.SelectNodes("//select[@name='listeurl']");
            foreach (HtmlNode select in nodes)
            {
                foreach (HtmlNode option in select.Elements("option"))
                {
                    string url = option.Attributes["value"].Value;
                    if (!String.IsNullOrWhiteSpace(url))
                        urls.Add(url);
                }
            }
            return urls;
        }

        private static HtmlDocument GetPage(Uri url)
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

        public class SeriePageExtractor
        {
            public IEnumerable<string> CollectSeasonsUrls(Uri page)
            {
                List<string> urls = new List<string>();
                string path = page.AbsolutePath;

                urls.Add(path); // saison 1, extrapolate other url and then test existence
                return urls;
            }
        }
    }
}