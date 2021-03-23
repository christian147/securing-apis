using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceServer.Constants;
using ResourceServer.Models;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ClaimsController : ControllerBase
    {
        [HttpGet]
        public async Task<ApiResponse> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var role = HttpContext.User.FindFirst(JwtClaimTypes.Role).Value;
            var subject = HttpContext.User.FindFirst(JwtClaimTypes.Subject).Value;
            var email = HttpContext.User.FindFirst(JwtClaimTypes.Email).Value;
            var organizationId = HttpContext.User.FindFirst(CustomClaimType.OrganizationId).Value;

            return new ApiResponse
            {
                Message = new { role, subject, email, organizationId }
            };
        }
    }
}
