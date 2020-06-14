using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WebLab.Areas.Identity.IdentityHostingStartup))]
namespace WebLab.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}