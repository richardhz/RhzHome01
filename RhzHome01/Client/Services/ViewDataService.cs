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

namespace RhzHome01.Client.Services
{
    public class ViewDataService : IRhzViewData
    {
        private readonly HttpClient _httpClient;
        private readonly RhzSettings settings;

        public ViewDataService(HttpClient client, IConfiguration Config)
        {
            _httpClient = client;
            settings = Config.GetSection("RhzSettings").Get<RhzSettings>();
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response) where T : class
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    var data = await response.Content?.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        return JsonConvert.DeserializeObject<T>(data);
                    }
                }
                catch (Exception ex)
                {
                    var e = ex.Message;
                    throw;
                }
            }
            return null;
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
            var response = await _httpClient.GetAsync($"{settings.BaseUrl}index?{settings.IndexKey}").ConfigureAwait(false);
            var data = await ProcessResponse<BasicContentViewModel>(response).ConfigureAwait(false);

            return new IndexData {
                MaxAge = settings.MaxCacheAge,
                PageData = data.Content.FirstOrDefault(d => d.Key == "hero").Value,
                SkillsData = data.Content.FirstOrDefault(d => d.Key == "skills").Value
            };
        }

        public async Task<AboutData> GetAboutViewModel()
        {
            var response = await _httpClient.GetAsync($"{settings.BaseUrl}about?{settings.AboutKey}").ConfigureAwait(false);
            var data = await ProcessResponse<BasicContentViewModel>(response).ConfigureAwait(false);

            return new AboutData
            {
                MaxAge = settings.MaxCacheAge,
                PageData = data.Content.FirstOrDefault(d => d.Key == "about").Value,
                InterestingLinks = data.Lists.FirstOrDefault(d => d.Key == "interestinglinks").Value,
                DotNetLinks = data.Lists.FirstOrDefault(d => d.Key == "dotnetlinks").Value
            };
        }

        public async Task<DocumentListData> GetDocumentsViewModel()
        {
            var response = await _httpClient.GetAsync($"{settings.BaseUrl}documents?{settings.DocListKey}").ConfigureAwait(false);
            var data = await ProcessResponse<BasicContentViewModel>(response).ConfigureAwait(false);

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
            var response = await _httpClient.GetAsync($"{settings.BaseUrl}documents/{key}?{settings.DocKey}").ConfigureAwait(false);
            var data = await ProcessResponse<BasicContentViewModel>(response).ConfigureAwait(false);
            return data?.Content["document"];
        }

        public async Task SendMessage(ContactModel message)
        {
            _ = await _httpClient.PostAsync($"{settings.BaseUrl}mail?{settings.MailKey}", PackageToPost(message)).ConfigureAwait(false);

        }
    }
}
