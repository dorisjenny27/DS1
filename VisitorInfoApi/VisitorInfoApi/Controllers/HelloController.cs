// Controllers/HelloController.cs
using Microsoft.AspNetCore.Mvc;
using VisitorInfoApi.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace VisitorInfoApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly IVisitorInfoService _visitorInfoService;
        private readonly ILogger<HelloController> _logger;

        public HelloController(IVisitorInfoService visitorInfoService, ILogger<HelloController> logger)
        {
            _visitorInfoService = visitorInfoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "visitor_name")] string visitorName)
        {
            try
            {
                string ipAddress = GetClientIpAddress();

                // calls service to get visitor info
                var result = await _visitorInfoService.GetVisitorInfo(ipAddress, visitorName);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Unable to process request", message = ex.Message });
            }
        }

        private string GetClientIpAddress()
        {
            var baseIp = "105.112.119.81";
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? baseIp;

            if (clientIp == "::1")
            {
                clientIp = baseIp;
            }

            if (clientIp.Contains("::ffff:"))
            {
                clientIp = clientIp.Replace("::ffff:", "");
            }

            return clientIp;
        }
    }
}