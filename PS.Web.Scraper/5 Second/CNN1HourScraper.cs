using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PS.Shared.HttpClients;
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
            var doc = await website.LoadFromWebAsync(sourceURL);
            var docNode = doc.DocumentNode;
            var tester = docNode.QuerySelectorAll(".pg-no-rail");
            foreach (var node in tester)
            {
                try
                {
                }
                // Try to upload the new scraped text data
                //await client.SetScraperText(new ScraperText { Text = headlinerText, Website = nodeURL });
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