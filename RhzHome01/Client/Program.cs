using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RhzHome01.Client.Services.Interfaces;
using RhzHome01.Client.Services;
using RhzHome01.Shared;
using Microsoft.Extensions.Configuration;
using MatBlazor;

namespace RhzHome01.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddMatBlazor();
            builder.Services.AddHttpClient("ServerlessApi",
                cli => {
                    var settings = builder.Configuration.GetSection("RhzSettings").Get<RhzSettings>();
                    cli.BaseAddress = new Uri(settings.BaseUrl);
                    cli.DefaultRequestHeaders.Add(settings.Key, settings.Value);
                });
            
            builder.Services.AddSingleton<ICacheService, CacheService>();
            builder.Services.AddTransient<IRhzViewData, ViewDataService>();
            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
