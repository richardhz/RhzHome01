using Microsoft.AspNetCore.Components;
using RhzHome01.Client.Services.Interfaces;
using RhzHome01.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RhzHome01.Client.Pages
{
    public class ContactBase : ComponentBase
    {
        [Inject]
        protected IRhzViewData ViewDataService { get; set; }
        [Inject] ICacheService CacheService { get; set; }
        protected ContactModel ContactData { get; set; } = new ContactModel();
        protected string Message { get; set; }
        protected bool Sent { get; set; }
        protected string StatusClass { get; set; }
        protected IEnumerable<LinkContentDto> InterestingLinks { get; set; }
        protected IEnumerable<LinkContentDto> DotNetLinks { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Sent = false;
            var Data = CacheService.Get<AboutData>("AboutData");
            if (Data == null)
            {
                Data = await ViewDataService.GetAboutViewModel().ConfigureAwait(false);  //looking at how ConfigureAwait false works with displays; seems to work here
                CacheService.Add("AboutData", Data, Data.MaxAge);   
            }
            
            InterestingLinks = Data.InterestingLinks;
            DotNetLinks = Data.DotNetLinks;
        }

        protected async Task HandleValidSubmit()
        {
            await ViewDataService.SendMessage(ContactData);
            Message = "Message Sent.";
            StatusClass = "alert-success";
            Sent = true;
            return; 
        }
    }
}
