using Microsoft.AspNetCore.Components;
using RhzHome01.Client.Services.Interfaces;
using RhzHome01.Shared;

namespace RhzHome01.Client.Components
{
    public class DocumentRowBase : ComponentBase
    {
        [Inject]
        protected IRhzViewData ViewDataService { get; set; }
        [Inject] NavigationManager NavManager { get; set; }
        [Parameter]
        public PostContentDto Document { get; set; }

        protected void GetDocument()
        {
            NavManager.NavigateTo($"/documents/{Document.BlobName}");
        }
    }
}
