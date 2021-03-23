﻿using Microsoft.AspNetCore.Mvc;
using ResourceServer.Models;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    public class PoliciesController : ControllerBase
    {
        [HttpGet("authorized")]
        public ApiResponse IsAuthorized() => new() { Message = "You are authorized!" };

        [HttpGet("administrator")]
        public ApiResponse IsAdministrator() => new() { Message = "You are administrator!" };

        [HttpGet("user")]
        public ApiResponse IsUser() => new() { Message = "You are user!" };

        [HttpGet("send-email")]
        public ApiResponse SendEmail() => new() { Message = "You has sent an email!" };

        [HttpGet("migrator")]
        public ApiResponse Migrate() => new() { Message = "The migration is done!" };

        [HttpGet("api-key")]
        public ApiResponse Get() => new() { Message = "The ApiKey is valid!" };
    }
}
