using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace ResourceServer.IntegrationTests
{
    public class TestWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
            => WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>();

        protected override TestServer CreateServer(IWebHostBuilder builder)
            => base.CreateServer(builder);

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseContentRoot(Directory.GetCurrentDirectory());
        }
    }
}
