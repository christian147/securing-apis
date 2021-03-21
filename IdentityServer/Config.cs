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
                    ClientId = "client.codepcke.selfcontained",
                    ClientName = "Code + PKCE (SelfContained)",
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
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone
                    },
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AccessTokenLifetime = 300,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = true
                },
                new Client
                {
                    ClientId = "client.codepcke.referential",
                    ClientName = "Code + PKCE (Referential)",
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
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone
                    },
                    AccessTokenType = AccessTokenType.Reference,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AccessTokenLifetime = 300,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = true
                },
                new Client
                {
                    ClientId = "client.clientcredentials.selfcontained",
                    ClientName = "Client Credentials (SelfContained)",
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
                    Scopes = { "api", "email.write", "migration" }
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
                    //Profile Scope
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.BirthDate, DateTime.UtcNow.AddYears(-16).ToShortDateString()),

                    //Email Scope
                    new Claim(JwtClaimTypes.EmailVerified, true.ToString(), ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),

                    //Phone Scope
                    new Claim(JwtClaimTypes.PhoneNumber, "+34123456789"),
                    new Claim(JwtClaimTypes.PhoneNumberVerified, true.ToString(), ClaimValueTypes.Boolean),

                    new Claim(JwtClaimTypes.Role, Roles.User),
                    new Claim(CustomClaimTypes.OrganizationId, Guid.NewGuid().ToString())
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
                    //Profile Scope
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.BirthDate, DateTime.UtcNow.AddYears(-30).ToShortDateString()),

                    //Email Scope
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, true.ToString(), ClaimValueTypes.Boolean),

                    //Phone Scope
                    new Claim(JwtClaimTypes.PhoneNumber, "+34123456789"),
                    new Claim(JwtClaimTypes.PhoneNumberVerified, true.ToString(), ClaimValueTypes.Boolean),


                    new Claim(JwtClaimTypes.Role, Roles.Administrator),
                    new Claim(CustomClaimTypes.OrganizationId, Guid.NewGuid().ToString())
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
            new ApiScope("api", "Allow to access to Protected Server", 
                new string[] {
                    JwtClaimTypes.BirthDate,
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