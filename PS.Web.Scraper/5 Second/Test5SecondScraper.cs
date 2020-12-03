using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PS.Shared.HttpClients;
using PS.Shared.Models;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper._5_Second
{
    public class Test5SecondScraper : BaseScraper, ITestWebScraper
    {
        public IList<ScraperText> ScrapeResults { get; set; }

        protected override async Task Scrape(ScraperTextClient client, HtmlWeb website, ILogger logger, CancellationToken token)
        {
            var watch = Stopwatch.StartNew();
            logger.LogInformation("About to fox politics Unlimited");
            ScrapeResults = new List<ScraperText>();
            string sourceURL = "https://www.foxnews.com/politics";
            var doc = await website.LoadFromWebAsync(sourceURL);
            var docNode = doc.DocumentNode;
            var test = docNode.QuerySelectorAll(".info .title");
            foreach (var node in test)
            {
                var relativeURL = node.QuerySelector("a").Attributes["href"].Value;
                var nodeURL = sourceURL + relativeURL;
                var headlinerText = node.InnerText;
                logger.LogInformation(headlinerText);
                logger.LogInformation(nodeURL);
                ScrapeResults.Add(new ScraperText()
                {
                    Website = nodeURL,
                    Text = headlinerText
                });
            }
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            logger.LogInformation($"Scrapped UB Unlimited in {elapsedTime} ms");
        }
    }
}