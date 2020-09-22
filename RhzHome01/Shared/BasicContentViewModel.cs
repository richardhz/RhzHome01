using System;
using System.Collections.Generic;

namespace RhzHome01.Shared
{
    [Serializable]
    public class BasicContentViewModel
    {
        public BasicContentViewModel()
        {
            Content = new Dictionary<string, string>();
            Lists = new Dictionary<string, IEnumerable<LinkContentDto>>();
            Documents = new List<PostContentDto>();
        }
        public string RequestPath { get; set; }
        public Dictionary<string, string> Content { get; set; }
        public Dictionary<string, IEnumerable<LinkContentDto>> Lists { get; set; }
        public IEnumerable<PostContentDto> Documents { get; set; }

    }
}
