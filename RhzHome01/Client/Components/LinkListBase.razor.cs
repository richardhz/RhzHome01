using Microsoft.AspNetCore.Components;
using Rhz.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
