﻿using System;

namespace RhzHome01.Shared
{
    [Serializable]
    public class LinkContentDto
    {
        public string Caption { get; set; }
        public string Target { get; set; }
        public string Url { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Published { get; set; }
    }
}
