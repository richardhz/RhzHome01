using Microsoft.AspNetCore.Components;
using RhzHome01.Client.Components;
using RhzHome01.Client.Services.Interfaces;
using RhzHome01.Shared;
namespace RhzHome01.Client.Pages;

public partial class Index
{
    [Inject]
    protected IRhzViewData ViewDataService { get; set; }
    [Inject] ICacheService CacheService { get; set; }
    protected string MainTitle { get; set; }
    protected string HeroData { get; set; }
    protected string SkillsData { get; set; }
    protected SkillsDialog SkillsDialog { get; set; }
    private IndexData Data { get; set; }
    protected string LoadingMessage { get; set; } = "Connecting to Azure functions...";

    protected override async Task OnInitializedAsync()
    {
        Data = CacheService.Get<IndexData>("IndexData");
        if (Data == null)
        {
            Data = await ViewDataService.GetIndexViewModel().ConfigureAwait(false);
            CacheService.Add("IndexData", Data, Data.MaxAge);
            LoadingMessage = "Loading";
        }

        HeroData = Data.PageData;
        SkillsData = Data.SkillsData;
        MainTitle = "Richard Hernandez";
    }

    protected void ShowSkillSummary()
    {
        SkillsDialog.Show(SkillsData);
    }
}
