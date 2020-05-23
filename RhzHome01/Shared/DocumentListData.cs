using System.Collections.Generic;

namespace RhzHome01.Shared
{
    public class DocumentListData : ContentBase
    {
        public IEnumerable<PostContentDto> Documents { get; set; }
        public IEnumerable<LinkContentDto> InterestingLinks { get; set; }
        public IEnumerable<LinkContentDto> DotNetLinks { get; set; }
    }
}
