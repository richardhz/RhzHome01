using RhzHome01.Client.Services.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        //private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _clientFactory;
        private readonly RhzSettings settings;

        JsonSerializerOptions jso = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ViewDataService(IHttpClientFactory factory, IConfiguration Config)
        {
            //_httpClient = client;
            _clientFactory = factory;
            settings = Config.GetSection("RhzSettings").Get<RhzSettings>();
        }


        private ByteArrayContent PackageToPost<T>(T data) where T : class
        {
            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }

        public async Task<IndexData> GetIndexViewModel()
        {
            var client = _clientFactory.CreateClient("ServerlessApi");
            var data = await client.GetFromJsonAsync<BasicContentViewModel>($"index?{ settings.IndexKey}",jso).ConfigureAwait(false);

            return new IndexData {
                MaxAge = settings.MaxCacheAge,
                PageData = data.Content.FirstOrDefault(d => d.Key == "hero").Value,
                SkillsData = data.Content.FirstOrDefault(d => d.Key == "skills").Value
            };
        }

        public async Task<AboutData> GetAboutViewModel()
        {
            var client = _clientFactory.CreateClient("ServerlessApi");
            var data = await client.GetFromJsonAsync<BasicContentViewModel>($"about?{ settings.AboutKey}",jso).ConfigureAwait(false);

            return new AboutData
            {
                MaxAge = settings.MaxCacheAge,
                PageData = data.Content.FirstOrDefault(d => d.Key == "about").Value,
                InterestingLinks = data.Lists.FirstOrDefault(d => d.Key == "interestinglinks").Value,
                DotNetLinks = data.Lists.FirstOrDefault(d => d.Key == "dotnetlinks").Value,
                ContactStatus = data.Lists.FirstOrDefault(d => d.Key == "mailstatus").Value?.FirstOrDefault()?.Caption
                
            };
        }

        public async Task<DocumentListData> GetDocumentsViewModel()
        {
            var client = _clientFactory.CreateClient("ServerlessApi");
            var data = await client.GetFromJsonAsync<BasicContentViewModel>($"documents?{ settings.DocListKey}",jso).ConfigureAwait(false);

            return new DocumentListData
            {
                MaxAge = settings.MaxCacheAge,
                Documents = data.Documents,
                InterestingLinks = data.Lists.FirstOrDefault(d => d.Key == "interestinglinks").Value,
                DotNetLinks = data.Lists.FirstOrDefault(d => d.Key == "dotnetlinks").Value
            };
        }

        public async Task<string> GetDocument(string key)
        {
            var client = _clientFactory.CreateClient("ServerlessApi");
            var data = await client.GetFromJsonAsync<BasicContentViewModel>($"documents/{key}?{ settings.DocKey}",jso).ConfigureAwait(false);
            return data?.Content["document"];
        }

        public async Task SendMessage(ContactModel message)
        {
            var client = _clientFactory.CreateClient("ServerlessApi");
            //_ = await _httpClient.PostAsync($"{settings.BaseUrl}mail?{settings.MailKey}", PackageToPost(message)).ConfigureAwait(false);
            _ = await client.PostAsync($"{client.BaseAddress}mail?{settings.MailKey}", PackageToPost(message)).ConfigureAwait(false);

        }
    }
}
