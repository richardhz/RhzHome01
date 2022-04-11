using Microsoft.AspNetCore.Components;
using RhzHome01.Client.Services.Interfaces;
using RhzHome01.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RhzHome01.Client.Pages
{
    public class DocumentListBase : ComponentBase
    {
        [Inject]
        NavigationManager NavManager { get; set; }
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
                Data = await ViewDataService.GetDocumentsViewModel().ConfigureAwait(false);
                CacheService.Add("Documents", Data, Data.MaxAge);   
            }

            Documents = Data.Documents;
            InterestingLinks = Data.InterestingLinks;
            DotNetLinks = Data.DotNetLinks;
        }

        protected void GotoPage(string name)
        {
           NavManager.NavigateTo($"/documents/{name}");
        }
    }
}
