using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhzHome01.Client.Components
{
    public class SkillsDialogBase : ComponentBase
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
}
