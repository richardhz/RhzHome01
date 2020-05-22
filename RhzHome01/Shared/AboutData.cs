using Rhz.Domains.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhzHome01.Shared
{
    public class AboutData : ContentBase
    {
        public string PageData { get; set; }
        public IEnumerable<LinkContentDto> InterestingLinks { get; set; }
        public IEnumerable<LinkContentDto> DotNetLinks { get; set; }
    }
}
