using System;
using System.Collections.Generic;
using System.Text;

namespace RhzHome01.Shared
{
    public class RhzSettings
    {
        public string BaseUrl { get; set; }
        public string AboutKey { get; set; }
        public string IndexKey { get; set; }
        public string DocListKey { get; set; }
        public string DocKey { get; set; }
        public string MailKey { get; set; }
        public int MaxCacheAge { get; set; }
    }
}
