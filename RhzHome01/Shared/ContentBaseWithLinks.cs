using System.Collections.Generic;

namespace RhzHome01.Shared
{
    public class ContentBaseWithLinks : ContentBase
    {
        public IEnumerable<LinkContentDto> InterestingLinks { get; set; }
        public IEnumerable<LinkContentDto> DotNetLinks { get; set; }
    }
}
