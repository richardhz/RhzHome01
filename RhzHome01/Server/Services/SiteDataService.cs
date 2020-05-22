using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rhz.Domains.Models;
using RhzHome01.Server.Services.Interfaces;
using RhzHome01.Shared;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RhzHome01.Server.Services
{
    public class SiteDataService : IRhzSiteData
    {
        private readonly HttpClient httpClient;
        private readonly RhzSettings settings;

        public SiteDataService(HttpClient client, IOptions<RhzSettings> options)
        {
            settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
            httpClient = client;
        }


        private async Task<T> ProcessResponse<T>(HttpResponseMessage response) where T : class
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = await response.Content?.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(data))
                {
                    return JsonConvert.DeserializeObject<T>(data);
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

        public async Task<BasicContentViewModel> GetHeroViewModel()
        {
            var response = await httpClient.GetAsync($"{settings.BaseUrl}index?{settings.IndexKey}");
            return await ProcessResponse<BasicContentViewModel>(response);
        }

        public async Task<BasicContentViewModel> GetAboutViewModel()
        {
            var response = await httpClient.GetAsync($"{settings.BaseUrl}about?{settings.AboutKey}");
            return await ProcessResponse<BasicContentViewModel>(response);
        }



        public async Task<BasicContentViewModel> GetDocumentsViewModel()
        {
            var response = await httpClient.GetAsync($"{settings.BaseUrl}documents?{settings.DocListKey}");
            return await ProcessResponse<BasicContentViewModel>(response);
        }

        public async Task<BasicContentViewModel> GetDocument(string key)
        {
            var response = await httpClient.GetAsync($"{settings.BaseUrl}documents/{key}?{settings.DocKey}");
            return await ProcessResponse<BasicContentViewModel>(response);
        }

        public async Task SendMessage(ContactModel message)
        {
            _ = await httpClient.PostAsync($"{settings.BaseUrl}mail?{settings.MailKey}", PackageToPost(message));
        }
    }
}
