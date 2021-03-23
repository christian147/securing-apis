using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ResourceServer.Requirements
{
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
        public string ApiKey { get; }

        public ApiKeyRequirement(string apiKey)
        {
            ApiKey = apiKey;
        }
    }

    public class ApiKeyRequirementHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        private const string ApiKeyHeaderName = "x-api-key";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            if (context.Resource is DefaultHttpContext httpContext &&                
                httpContext.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey) &&
                string.Equals(requirement.ApiKey, apiKey, StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}