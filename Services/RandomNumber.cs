using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Practise_Tasks.Interfaces;
using Practise_Tasks.Settings;
using System.Runtime;
using System.Text;

namespace Practise_Tasks.Services
{
    public class RandomNumber : IRandomNumber
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RandomNumberSettings _settings;
        public RandomNumber(IHttpClientFactory httpClientFactory, IOptions<RandomNumberSettings> settings)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings.Value;
        }

        public async Task<int> GetIntAsync(int min, int max)
        {
            var client = _httpClientFactory.CreateClient("RandomNumberClient");
            var url = $"{_settings.BaseUrl}min={min}&max={max}&count=1";
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadFromJsonAsync<int[]>();
                return content?.FirstOrDefault() ?? new Random().Next(min, max);
            }
            catch (HttpRequestException)
            {
                return new Random().Next(min, max);
            }
        }
    }
}
