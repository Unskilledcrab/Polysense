using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PS.Shared.HttpClients;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper._5_Second
{
    public class Test5SecondScraper : BaseScraper, ITestWebScraper
    {
        protected override async Task Scrape(ScraperTextClient client, HtmlWeb website, ILogger logger, CancellationToken token)
        {
            var watch = Stopwatch.StartNew();
            logger.LogInformation("About to scrape UB Unlimited");

            var doc = await website.LoadFromWebAsync("https://www.ubunlimited.com");

            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            logger.LogInformation($"Scrapped UB Unlimited in {elapsedTime} ms");
        }
    }
}