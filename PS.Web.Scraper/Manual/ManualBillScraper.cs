using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PS.Shared.HttpClients;
using PS.Web.Scraper.Abstractions;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper.Manual
{
    public class ManualBillScraper : BaseScraper
    {
        protected override Task Scrape(ScraperTextClient client, HtmlWeb website, ILogger logger, CancellationToken token)
        {
            var watch = Stopwatch.StartNew();
            logger.LogInformation("About to scrape CNN Politics");
            string sourceURL = "https://www.govtrack.us/congress/bills/browse?status=28,29,32,33&sort=-current_status_date";
            IWebDriver driver = new ChromeDriver();
            driver.Url = sourceURL;
            var enactedLinks = driver.FindElements(By.ClassName("col-xs-10 col-md-11"));
            foreach (var element in enactedLinks)
            {
                var url = element.FindElement(By.TagName("a")).GetAttribute("href");
            }
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            logger.LogInformation($"Scrapped CNN Politics in {elapsedTime} ms");
            return Task.CompletedTask;
        }
    }
}