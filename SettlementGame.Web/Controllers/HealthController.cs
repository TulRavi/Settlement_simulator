using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SettlementGame.Web.Controllers
{
    
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // This endpoint is used by the WinForms client
            // to check whether the Web API is ready to accept requests.
            return Ok("SettlementGame.Web is running");
        }
    }
}
