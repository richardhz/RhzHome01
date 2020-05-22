using Microsoft.AspNetCore.Components;
using RhzHome01.Client.Services.Interfaces;
using System.Threading.Tasks;

namespace RhzHome01.Client.Pages
{
    public class DocumentBase : ComponentBase
    {
        [Inject]
        protected IRhzViewData ViewDataService { get; set; }
        //[Inject]
        //protected CacheService CacheService { get; set; }
        [Parameter]
        public string Key { get; set; }
        public string Text { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //Text = (string)CacheService.Get(Key);
            //if (Text == null)
            {
                Text = await ViewDataService.GetDocument(Key).ConfigureAwait(false);  //looking at how ConfigureAwait false works with displays; seems to work here
                if (Text == null)
                {
                    Text = $"{Key} does not exist.";
                    return;
                }
                //CacheService.Add(Key, Text, 1);   //get the maxage in minutes from config
            }


        }
    }
}
