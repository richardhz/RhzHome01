using System.Collections.Generic;

namespace RhzHome01.Shared
{
    public record AboutData : ContentBaseWithLinks
    {
        public string PageData { get; init; }
        public string ContactStatus { get; init; }
    }
}
