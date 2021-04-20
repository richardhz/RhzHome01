using RhzHome01.Client.Services.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RhzHome01.Shared;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
//using RhzHome01.Client.Extensions;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text.Json;

namespace RhzHome01.Client.Services
{
    public class ViewDataService : IRhzViewData
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly int _cacheAge;
        private const int _defaultCacheAge = 60;

        private readonly JsonSerializerOptions jso = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ViewDataService(IHttpClientFactory factory, IConfiguration Config)
        {
            _clientFactory = factory;
            _cacheAge = Config.GetSection("RhzSettings").Get<RhzSettings>().MaxCacheAge;
            _cacheAge = (_cacheAge <= 0) ? _defaultCacheAge : _cacheAge;
        }


        public async Task<IndexData> GetIndexViewModel()
        {
            var data = await GetJsonData("index");

            return new IndexData {
                MaxAge = _cacheAge,
                PageData = data.Content.FirstOrDefault(d => d.Key == "hero").Value,
                SkillsData = data.Content.FirstOrDefault(d => d.Key == "skills").Value
            };
        }

        public async Task<AboutData> GetAboutViewModel()
        {
            var data = await GetJsonData("about");

            return new AboutData
            {
                MaxAge = _cacheAge,
                PageData = data.Content.FirstOrDefault(d => d.Key == "about").Value,
                InterestingLinks = data.Lists.FirstOrDefault(d => d.Key == "interestinglinks").Value,
                DotNetLinks = data.Lists.FirstOrDefault(d => d.Key == "dotnetlinks").Value,
                ContactStatus = data.Lists.FirstOrDefault(d => d.Key == "mailstatus").Value?.FirstOrDefault()?.Caption
                
            };
        }

        public async Task<DocumentListData> GetDocumentsViewModel()
        {
            var data = await GetJsonData("documents");

            return new DocumentListData
            {
                MaxAge = _cacheAge,
                Documents = data.Documents,
                InterestingLinks = data.Lists.FirstOrDefault(d => d.Key == "interestinglinks").Value,
                DotNetLinks = data.Lists.FirstOrDefault(d => d.Key == "dotnetlinks").Value
            };
        }

        public async Task<string> GetDocument(string key)
        {
            var data = await GetJsonData($"documents/{key}");

            return data?.Content["document"];
        }

        public async Task SendMessage(ContactModel message)
        {
            var client = _clientFactory.CreateClient("ServerlessApi");
            _ = await client.PostAsJsonAsync<ContactModel>("mail", message, jso).ConfigureAwait(false);

        }


        private async Task<BasicContentViewModel> GetJsonData(string endpoint)
        {
            var client = _clientFactory.CreateClient("ServerlessApi");
            var data = await client.GetFromJsonAsync<BasicContentViewModel>(endpoint, jso).ConfigureAwait(false);
            return data;
        }
    }
}
