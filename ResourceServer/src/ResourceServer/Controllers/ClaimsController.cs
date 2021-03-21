using Microsoft.AspNetCore.Mvc;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok();
        }
    }
}
