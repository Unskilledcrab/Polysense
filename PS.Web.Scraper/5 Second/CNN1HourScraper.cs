using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PS.Shared.HttpClients;
using PS.Shared.Models;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper._5_Second
{
    public class CNN1HourScraper : BaseScraper, ITestWebScraper
    {
        protected async override Task Scrape(ScraperTextClient client, HtmlWeb website, ILogger logger, CancellationToken token)
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
                //resultsList.Add(new ScraperText()
                //{
                //  Website = url,
                //  Text = headlineText
                //});

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