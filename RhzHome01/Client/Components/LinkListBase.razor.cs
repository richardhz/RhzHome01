using Microsoft.AspNetCore.Components;
using RhzHome01.Shared;
using System.Collections.Generic;

namespace RhzHome01.Client.Components
{
    public class LinkListBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public IEnumerable<LinkContentDto> LinkData { get; set; }
    }
}
