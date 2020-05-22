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
    public class DocumentListBase : ComponentBase
    {
        [Inject]
        protected IRhzViewData ViewDataService { get; set; }
        [Inject] ICacheService CacheService { get; set; }
        protected IEnumerable<PostContentDto> Documents { get; set; }
        protected IEnumerable<LinkContentDto> InterestingLinks { get; set; }
        protected IEnumerable<LinkContentDto> DotNetLinks { get; set; }
        private DocumentListData Data { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Data = CacheService.Get<DocumentListData>("Documents");
            if (Data == null)
            {
                Data = await ViewDataService.GetDocumentsViewModel();
                CacheService.Add("Documents", Data, Data.MaxAge);   //get the maxage in seconds from server
            }

            Documents = Data.Documents;
            InterestingLinks = Data.InterestingLinks;
            DotNetLinks = Data.DotNetLinks;
        }
    }
}
