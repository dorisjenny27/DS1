using System.Net.Http.Json;
using VisitorInfoApi.Models;
using VisitorInfoApi.Models.DTOs;
using VisitorInfoApi.Services.Interfaces;

namespace VisitorInfoApi.Services
{
    public class VisitorInfoService : IVisitorInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public VisitorInfoService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<VisitorInfo> GetVisitorInfo(string ipAddress, string visitorName)
        {
            var ipStackResponse = await GetIpStackInfo(ipAddress);
            var weatherResponse = await GetWeatherInfo(ipStackResponse.Latitude, ipStackResponse.Longitude);

            return new VisitorInfo
            {
                ClientIp = ipAddress,
                Location = ipStackResponse.City,
                Greeting = $"Hello, {visitorName}! The temperature is {weatherResponse.Data.Values.Temperature:F1} degrees Celsius in {ipStackResponse.City}"
            };
        }

        private async Task<IpStackResponse> GetIpStackInfo(string ipAddress)
        {
            var baseUrl = _configuration["APISettings:BaseUrl"];
            var apiKey = _configuration["APISettings:ApiKey"];
            var url = $"{baseUrl}{ipAddress}?access_key={apiKey}";
            var response = await _httpClient.GetFromJsonAsync<IpStackResponse>(url);

            if (response == null || string.IsNullOrEmpty(response.City))
            {
                throw new Exception("Unable to determine location from IP address");
            }

            return response;
        }

        private async Task<TomorrowioResponse> GetWeatherInfo(double latitude, double longitude)
        {
            var baseUrl = _configuration["Tomorrowio:BaseUrl"];
            var apiKey = _configuration["Tomorrowio:ApiKey"];
            var url = $"{baseUrl}?location={latitude},{longitude}&apikey={apiKey}";
            return await _httpClient.GetFromJsonAsync<TomorrowioResponse>(url);
        }
    }
}