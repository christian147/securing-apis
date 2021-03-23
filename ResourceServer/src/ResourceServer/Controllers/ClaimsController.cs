using Microsoft.AspNetCore.Mvc;
using ResourceServer.Models;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        [HttpGet]
        public ApiResponse Get() => new() { Message = "no claims" };
    }
}
