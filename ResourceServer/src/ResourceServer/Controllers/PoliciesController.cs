using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceServer.Constants;
using ResourceServer.Models;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    public class PoliciesController : ControllerBase
    {
        [HttpGet("authorized")]
        [Authorize]
        public ApiResponse IsAuthorized() => new() { Message = "You are authorized!" };

        [HttpGet("administrator")]
        [Authorize(Roles = Role.Administrator)]
        public ApiResponse IsAdministrator() => new() { Message = "You are administrator!" };

        [HttpGet("user")]
        [Authorize(Roles = Role.User)]
        public ApiResponse IsUser() => new() { Message = "You are user!" };

        [HttpGet("send-email")]
        [Authorize(Policy = Policy.SendEmails)]
        public ApiResponse SendEmail() => new() { Message = "You has sent an email!" };

        [HttpGet("migrator")]
        [Authorize(Policy = Policy.Migration)]
        public ApiResponse Migrate() => new() { Message = "The migration is done!" };

        [HttpGet("api-key")]
        [Authorize(Policy = Policy.ApiKey)]
        public ApiResponse Get() => new() { Message = "The ApiKey is valid!" };
    }
}
