using System;

namespace RhzHome01.Shared
{
    public record PostContentDto
    {
        //public string Key { get; set; }
        public string Caption { get; init; }
        public string Preview { get; init; }
        public string Content { get; init; }
        public DateTime PublishedOn { get; init; }
        public DateTime UpdatedOn { get; init; }
        public bool Published { get; init; }
        public string BlobName { get; init; }
    }
}
