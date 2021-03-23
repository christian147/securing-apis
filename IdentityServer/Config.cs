using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityModel;
using IdentityServer.Constants;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone()
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "client.codepcke",
                    ClientName = "Code + PKCE",
                    //AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenType = AccessTokenType.Reference,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200/login" },
                    RedirectUris = {
                        "http://localhost:4200/silent-renew-oidc.html",
                        "http://localhost:4200/callback",
                        "http://localhost:4200"
                    },
                    AllowedScopes = {
                        "api",
                        "email.write",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AccessTokenLifetime = 300,
                    AlwaysIncludeUserClaimsInIdToken = true
                },
                new Client
                {
                    ClientId = "jobclient.clientcredentials",
                    ClientName = "Job Client Credentials",
                    RequireClientSecret = true,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = {
                        "migration"
                    },
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AccessTokenLifetime = 300
                }
            };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
             new ApiResource
                {
                    Name = "api",
                    DisplayName = "Protected Service",
                    Description = "Access to Protected Service",
                    ApiSecrets = { new Secret("secret".Sha256()) },
                    Enabled = true,
                    Scopes = { "email.write", "migration" }
                }
        };

        public static IEnumerable<TestUser> TestUsers => new TestUser[]
        {
            new TestUser
            {
                Username = "bob",
                Password = "bob",
                SubjectId =  Guid.NewGuid().ToString(), 
                IsActive = true,
                Claims = new List<Claim>
                {

                    new Claim(JwtClaimTypes.Role, Roles.User),
                    new Claim(CustomClaimTypes.OrganizationId, Guid.NewGuid().ToString()),

                    //Profile Scope
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),

                    //Email Scope
                    new Claim(JwtClaimTypes.EmailVerified, true.ToString(), ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com")
                }
            },
            new TestUser
            {
                Username = "alice",
                Password = "alice",
                SubjectId =  Guid.NewGuid().ToString(),
                IsActive = true,
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Role, Roles.Administrator),
                    new Claim(CustomClaimTypes.OrganizationId, Guid.NewGuid().ToString()),

                    //Profile Scope
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),

                    //Email Scope
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, true.ToString(), ClaimValueTypes.Boolean)
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
            new ApiScope("api", "Allow to access to Protected Server", 
                new string[] {
                    JwtClaimTypes.Role,
                    CustomClaimTypes.OrganizationId
                }) { Required = true },
            new ApiScope("email.write", "Allow to send emails",
                new string[] {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }) { Required = true },
            new ApiScope("migration")
        };
    }
}