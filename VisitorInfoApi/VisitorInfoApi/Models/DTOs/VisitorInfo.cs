using System.Text.Json.Serialization;

namespace VisitorInfoApi.Models.DTOs
{
    public class VisitorInfo
    {
        public string ClientIp { get; set; }
        public string Location { get; set; }
        public string Greeting { get; set; }
    }
}
