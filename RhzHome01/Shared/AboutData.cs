using System.Collections.Generic;

namespace RhzHome01.Shared
{
    public class AboutData : ContentBase
    {
        public string PageData { get; set; }
        public IEnumerable<LinkContentDto> InterestingLinks { get; set; }
        public IEnumerable<LinkContentDto> DotNetLinks { get; set; }
        public string ContactStatus { get; set; }
    }
}
