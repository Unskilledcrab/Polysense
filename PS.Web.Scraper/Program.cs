using Microsoft.Extensions.Hosting;
using PS.Shared.HttpClients;
using PS.Web.Scraper.Extensions;

namespace PS.Web.Scraper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClients();
                    services.AddWebScrapers();
                });
    }
}