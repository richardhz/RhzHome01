using Microsoft.AspNetCore.Components;
using RhzHome01.Client.Services.Interfaces;
using RhzHome01.Shared;
namespace RhzHome01.Client.Pages;

public partial class Contact
{
    [Inject]
    protected IRhzViewData ViewDataService { get; set; }
    [Inject] ICacheService CacheService { get; set; }
    protected ContactModel ContactData { get; set; } = new ContactModel();
    protected string Message { get; set; }
    protected bool Sent { get; set; }
    protected string StatusClass { get; set; }
    protected bool ButtonIsDisabled { get; set; }
    protected string StatusMessage { get; set; }
    protected IEnumerable<LinkContentDto> InterestingLinks { get; set; }
    protected IEnumerable<LinkContentDto> DotNetLinks { get; set; }

    /// <summary>
    /// This initialization is very convoluted, this is because I could not be bothered to separate the links from the about page 
    /// so I have to call about to get the links.
    /// I later decided to cache the contact to demonstrate how caching a form could be useful and because the contact is not held remotely
    /// I have to stitch the links to a new contact holding record, not nice.
    /// I will fix this later.
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Sent = false;
        var Data = CacheService.Get<ContactHolding>("ContactData");
        if (Data == null)
        {
            var DataAbout = CacheService.Get<AboutData>("AboutData");
            if (DataAbout == null)
            {
                DataAbout = await ViewDataService.GetAboutViewModel().ConfigureAwait(false);
                CacheService.Add("AboutData", DataAbout, DataAbout.MaxAge);
            }
            Data = new ContactHolding
            {
                MaxAge = 200,
                InterestingLinks = DataAbout.InterestingLinks,
                DotNetLinks = DataAbout.DotNetLinks,
                ContactStatus = DataAbout.ContactStatus
            };

            CacheService.Add("ContactData", Data, Data.MaxAge);
        }
        else
        {
            ContactData.Subject = Data.Subject;
            ContactData.Email = Data.Email;
            ContactData.Name = Data.Name;
            ContactData.Comment = Data.Comment;
        }

        InterestingLinks = Data.InterestingLinks;
        DotNetLinks = Data.DotNetLinks;
        StatusMessage = Data.ContactStatus;
        ButtonIsDisabled = Data.ContactStatus != null;
    }

    protected async Task HandleValidSubmit()
    {
        await ViewDataService.SendMessage(ContactData);
        Message = "Message Sent.";
        StatusClass = "alert-success";
        Sent = true;
        return;
    }

    void IDisposable.Dispose() 
    {
        if (!Sent)
        {
            var CData = CacheService.Get<ContactHolding>("ContactData");
            if (CData != null)
            {
                CData.Subject = ContactData.Subject;
                CData.Email = ContactData.Email;
                CData.Name = ContactData.Name;
                CData.Comment = ContactData.Comment;
                CacheService.Remove("ContactData");
                CacheService.Add("ContactData", CData, CData.MaxAge);
            }
        }
        else
        {
            var CData = CacheService.Get<ContactHolding>("ContactData");
            if (CData != null)
            {
                CacheService.Remove("ContactData");
            }
        }
    }
    
}
