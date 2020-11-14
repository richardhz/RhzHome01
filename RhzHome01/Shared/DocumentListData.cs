using System.Collections.Generic;

namespace RhzHome01.Shared
{
    public class DocumentListData : ContentBaseWithLinks
    {
        public IEnumerable<PostContentDto> Documents { get; set; }
    }
}
