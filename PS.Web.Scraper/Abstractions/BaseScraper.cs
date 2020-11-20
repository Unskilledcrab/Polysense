using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PS.Web.Scraper.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper.Abstractions
{
    abstract public class BaseScraper : IWebScraper
    {
        public async Task Scrape(ILogger logger, CancellationToken token)
        {
            HtmlWeb website = new HtmlWeb();
            try
            {
                await Scrape(website, logger, token);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "test");
            }
        }

        protected abstract Task Scrape(HtmlWeb website, ILogger logger, CancellationToken token);
    }
}