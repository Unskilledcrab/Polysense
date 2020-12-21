using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PS.Shared.HttpClients;
using PS.Shared.Models;
using PS.Web.Scraper.Abstractions;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper._5_Second
{
    public class Test5SecondScraper : BaseScraper//, ITestWebScraper
    {
        // Scraper Test
        // Tester: Jeremy Buentello Time:
        protected override async Task Scrape(ScraperTextClient client, HtmlWeb website, ILogger logger, CancellationToken token)
        {
            var watch = Stopwatch.StartNew();
            logger.LogInformation("About to scrape Fox Politics");
            string sourceURL = "https://www.foxnews.com/politics";
            var doc = await website.LoadFromWebAsync(sourceURL);
            var docNode = doc.DocumentNode;
            var test = docNode.QuerySelectorAll(".info .title");
            foreach (var node in test)
            {
                var relativeURL = node.QuerySelector("a").Attributes["href"].Value;
                var nodeURL = sourceURL + relativeURL;
                var headlinerText = node.InnerText;
                try
                {
                    // Try to upload the new scraped text data
                    await client.SetScraperText(new ScraperText { Text = headlinerText, Website = nodeURL });
                }
                catch (HttpRequestException ex)
                {
                    // If it is a conflict (already in the database) continue, otherwise throw the error.
                    if (ex.StatusCode != System.Net.HttpStatusCode.Conflict)
                        throw;
                }
            }
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            logger.LogInformation($"Scrapped Fox Politics in {elapsedTime} ms");
        }

        async private Task ScrapeCNN(ScraperTextClient client, HtmlWeb website, ILogger logger, CancellationToken token)
        {
            var watch = Stopwatch.StartNew();
            logger.LogInformation("About to scrape CNN Politics");
            string sourceURL = "https://www.cnn.com/politics";
            IWebDriver driver = new ChromeDriver();
            driver.Url = sourceURL;
            var results = driver.FindElements(By.ClassName("cd__headline"));
            foreach (var element in results)
            {
                var headlinerText = element.Text;
                var url = element.FindElement(By.TagName("a")).GetAttribute("href");
                try
                {
                    // Try to upload the new scraped text data
                    await client.SetScraperText(new ScraperText { Text = headlinerText, Website = url });
                }
                catch (HttpRequestException ex)
                {
                    // If it is a conflict (already in the database) continue, otherwise throw the error.
                    if (ex.StatusCode != System.Net.HttpStatusCode.Conflict)
                        throw;
                }
            }
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            logger.LogInformation($"Scrapped CNN Politics in {elapsedTime} ms");
        }
    }
}