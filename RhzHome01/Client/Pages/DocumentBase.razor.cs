using Microsoft.AspNetCore.Components;
using RhzHome01.Client.Services.Interfaces;
using System.Threading.Tasks;

namespace RhzHome01.Client.Pages
{
    public class DocumentBase : ComponentBase
    {
        [Inject]
        protected IRhzViewData ViewDataService { get; set; }
        [Parameter]
        public string Key { get; set; }
        public string Text { get; set; }

        protected override async Task OnInitializedAsync()
        {
            {
                Text = await ViewDataService.GetDocument(Key).ConfigureAwait(false);  
                if (Text == null)
                {
                    Text = $"{Key} does not exist.";
                    return;
                }
            }


        }
    }
}
