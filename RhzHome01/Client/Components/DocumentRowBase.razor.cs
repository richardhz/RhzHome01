using Microsoft.AspNetCore.Components;
using Rhz.Domains.Models;
using RhzHome01.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhzHome01.Client.Components
{
    public class DocumentRowBase : ComponentBase
    {
        [Inject]
        protected IRhzViewData ViewDataService { get; set; }
        //[Inject] CacheService CacheService { get; set; }
        [Inject] NavigationManager NavManager { get; set; }
        [Parameter]
        public PostContentDto Document { get; set; }

        protected void GetDocument()
        {
            //var hasItem = CacheService.Peek(Document.BlobName);
            //if (!hasItem)
            {
                //var docData = await ViewDataService.GetDocument(Document.BlobName); // we cannot call configureawait false here because this method is being called by the UI

               // CacheService.Add(Document.BlobName, docData, 1);
            }
            NavManager.NavigateTo($"/documents/{Document.BlobName}");
        }
    }
}
