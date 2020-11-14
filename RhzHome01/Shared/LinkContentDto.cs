using System;

namespace RhzHome01.Shared
{
    [Serializable]
    public record LinkContentDto
    {
        public string Caption { get; init; }
        public string Target { get; init; }
        public string Url { get; init; }
        public DateTime UpdatedOn { get; init; }
        public bool Published { get; init; }
    }
}
