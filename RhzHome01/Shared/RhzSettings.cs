using System;
using System.Collections.Generic;
using System.Text;

namespace RhzHome01.Shared
{
    public class RhzSettings
    {
        public string BaseUrl { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int MaxCacheAge { get; set; }
    }
}
