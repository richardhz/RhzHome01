using RhzHome01.Client.Services.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RhzHome01.Shared;
using System.Text;
using System.Net.Http.Headers;

namespace RhzHome01.Client.Services
{
    public class ViewDataService : IRhzViewData
    {
        private readonly HttpClient _httpClient;
        public ViewDataService(HttpClient client)
        {
            _httpClient = client;
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response) where T : class
        {
            // In all api calls we must check if the response was sucessful and act accordingly.

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

        

        private ByteArrayContent PackageToPost<T>(T data) where T: class
        {
            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }

        private int GetCacheMaxAge(HttpResponseMessage response)
        {
            double seconds = 0.0;
            if (response.Headers.CacheControl.MaxAge.HasValue)
            {
                seconds = response.Headers.CacheControl.MaxAge.Value.TotalSeconds;
            }
            return (int)seconds;
        }

        public async Task<IndexData> GetIndexViewModel()
        {
            var response = await _httpClient.GetAsync("SiteContent/IndexPage");
            var data = await ProcessResponse<BasicContentViewModel>(response);
            

            return new IndexData {
                MaxAge = GetCacheMaxAge(response),
                PageData = data.Content.FirstOrDefault(d => d.Key == "hero").Value,
                SkillsData = data.Content.FirstOrDefault(d => d.Key == "skills").Value
            };
            
        }

        public async Task<AboutData> GetAboutViewModel()
        {
            var response = await _httpClient.GetAsync("SiteContent/AboutPage");
            var data = await ProcessResponse<BasicContentViewModel>(response);

            return new AboutData
            {
                MaxAge = GetCacheMaxAge(response),
                PageData = data.Content.FirstOrDefault(d => d.Key == "about").Value,
                InterestingLinks = data.Lists.FirstOrDefault(d => d.Key == "interestinglinks").Value,
                DotNetLinks = data.Lists.FirstOrDefault(d => d.Key == "dotnetlinks").Value
            };
        }



        public async Task<DocumentListData> GetDocumentsViewModel()
        {
            var response = await _httpClient.GetAsync("SiteContent/Documents");
            var data = await ProcessResponse<BasicContentViewModel>(response);

            return new DocumentListData
            {
                MaxAge = GetCacheMaxAge(response),
                Documents = data.Documents,
                InterestingLinks = data.Lists.FirstOrDefault(d => d.Key == "interestinglinks").Value,
                DotNetLinks = data.Lists.FirstOrDefault(d => d.Key == "dotnetlinks").Value
            };
        }


        public async Task<string> GetDocument(string key)
        {
            var response = await _httpClient.GetAsync($"SiteContent/Documents/{key}");
            var data = await ProcessResponse<BasicContentViewModel>(response);
            return data?.Content["document"];
        }

        public async Task SendMessage(ContactModel message)
        {
            var response = await _httpClient.PostAsync($"SiteContent/mail",PackageToPost(message));
        }
    }
}
