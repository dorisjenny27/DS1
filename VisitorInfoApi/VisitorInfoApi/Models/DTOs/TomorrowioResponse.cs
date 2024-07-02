namespace VisitorInfoApi.Models.DTOs
{
    public class TomorrowioResponse
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public Values Values { get; set; }
    }

    public class Values
    {
        public double Temperature { get; set; }
    }
}
