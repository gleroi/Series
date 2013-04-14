using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Core.TvShows
{
    public class WithMetadata
    {
        public WithMetadata()
        {
            this.Metadatas = new Dictionary<string, object>();
        }

        public IDictionary<string, object> Metadatas { get; set; }
    }
}