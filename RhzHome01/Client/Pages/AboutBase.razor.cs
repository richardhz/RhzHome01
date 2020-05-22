using Microsoft.AspNetCore.Components;
using Rhz.Domains.Models;
using RhzHome01.Client.Services.Interfaces;
using RhzHome01.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhzHome01.Client.Pages
{
    public class AboutBase : ComponentBase
    {
        [Inject]
        protected IRhzViewData ViewDataService { get; set; }
        [Inject] ICacheService CacheService { get; set; }
        protected string AboutData { get; set; }
        protected IEnumerable<LinkContentDto> InterestingLinks { get; set; }
        protected IEnumerable<LinkContentDto> DotNetLinks { get; set; }
        private AboutData Data { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Data = CacheService.Get<AboutData>("AboutData");
            if (Data == null)
            {
                Data = await ViewDataService.GetAboutViewModel().ConfigureAwait(false);  //looking at how ConfigureAwait false works with displays; seems to work here
                CacheService.Add("AboutData", Data, Data.MaxAge);   
            }

            AboutData = Data.PageData;
            InterestingLinks = Data.InterestingLinks;
            DotNetLinks = Data.DotNetLinks;
        }
    }
}
