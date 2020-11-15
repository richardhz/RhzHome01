using System.Collections.Generic;

namespace RhzHome01.Shared
{
    public record ContentBaseWithLinks : ContentBase
    {
        public IEnumerable<LinkContentDto> InterestingLinks { get; init; }
        public IEnumerable<LinkContentDto> DotNetLinks { get; init; }
    }
}
