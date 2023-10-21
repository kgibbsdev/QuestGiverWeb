using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Microsoft.Extensions.Hosting;

namespace QuestGiver.Server
{
    public class StartupService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _services;
        private volatile bool _ready = false;


        public StartupService(IHttpClientFactory httpClientFactory, IServiceProvider services, IHostApplicationLifetime lifetime)
        {
            _httpClientFactory = httpClientFactory;
            _services = services;
            lifetime.ApplicationStarted.Register(() => _ready = true);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Reference: https://andrewlock.net/finding-the-urls-of-an-aspnetcore-app-from-a-hosted-service-in-dotnet-6/
            while (!_ready)
            {
                // App hasn't started yet, keep looping!
                await Task.Delay(1000);
            }

            await Startup();
        }

        private async Task Startup()
        {
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = null;
            int retryCount = 3;

            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    #if DEBUG
                    response = await client.PostAsync("http://localhost:5124/api/Quests/Startup", null);
                    #else
                    response = await client.PostAsync("https:/localhost:8080/api/Quests/Startup", null);
                    #endif 
                    if (response.IsSuccessStatusCode)
                    {
                        // Handle successful response if necessary
                        break;
                    }
                    else
                    {
                        // Handle error response if necessary
                    }
                }
                catch (HttpRequestException)
                {
                    if (i < retryCount - 1) // Not the last retry
                    {
                        await Task.Delay(2000); // Wait for 2 seconds before the next retry
                    }
                    else
                    {
                        throw; // Re-throw the exception on the last retry
                    }
                }
            }
        }
    }
}
