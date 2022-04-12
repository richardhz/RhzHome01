using Microsoft.AspNetCore.Components;
using RhzHome01.Client.Services.Interfaces;
using RhzHome01.Shared;
namespace RhzHome01.Client.Pages;

public partial class About
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
            Data = await ViewDataService.GetAboutViewModel().ConfigureAwait(false);
            CacheService.Add("AboutData", Data, Data.MaxAge);
        }

        AboutData = Data.PageData;
        InterestingLinks = Data.InterestingLinks;
        DotNetLinks = Data.DotNetLinks;
    }
}
