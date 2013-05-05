using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Series.Website.Api.Formatters
{
    public class FeedMediaTypeFormatter : MediaTypeFormatter
    {
        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return typeof(SyndicationFeed).IsAssignableFrom(type);
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                WriteFeed(value as SyndicationFeed, writeStream);
            });
        }

        private void WriteFeed(SyndicationFeed syndicationFeed, System.IO.Stream writeStream)
        {
            using (XmlWriter writer = XmlWriter.Create(writeStream))
            {
                SyndicationFeedFormatter formatter = new Rss20FeedFormatter(syndicationFeed, false);
                formatter.WriteTo(writer);
            }
        }
    }
}