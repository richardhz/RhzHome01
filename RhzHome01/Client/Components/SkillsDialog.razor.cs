namespace RhzHome01.Client.Components;

public partial class SkillsDialog
{
    public bool ShowDialog { get; set; }
    protected string SkillsData { get; set; }
    public void Show(string data)
    {
        SkillsData = data;
        ShowDialog = true;
        StateHasChanged();
    }

    public void Close()
    {
        ShowDialog = false;
        StateHasChanged();
    }
}
