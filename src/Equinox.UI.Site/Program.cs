using System.IO;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Equinox.UI.Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseContentRoot($"{Directory.GetCurrentDirectory()}/src/Equinox.UI.Site")
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 8001);
                })
                .UseEnvironment("Development")
                .Build();

    }
}
