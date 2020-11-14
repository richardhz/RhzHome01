using System.Collections.Generic;

namespace RhzHome01.Shared
{
    public record DocumentListData : ContentBaseWithLinks
    {
        public IEnumerable<PostContentDto> Documents { get; init; }
    }
}
