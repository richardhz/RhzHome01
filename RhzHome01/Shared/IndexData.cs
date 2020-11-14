using System;
using System.Collections.Generic;
using System.Text;

namespace RhzHome01.Shared
{
    public record IndexData : ContentBase
    {
        public string PageData { get; init; }
        public string SkillsData { get; init; }
    }
}
