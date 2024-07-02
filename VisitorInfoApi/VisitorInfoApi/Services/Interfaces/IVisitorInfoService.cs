using VisitorInfoApi.Models.DTOs;

namespace VisitorInfoApi.Services.Interfaces
{
    public interface IVisitorInfoService
    {
        Task<VisitorInfo> GetVisitorInfo(string ipAddress, string visitorName);
    }
}
