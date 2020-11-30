using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PS.Shared.HttpClients;
using PS.Web.Scraper.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper.Abstractions
{
    abstract public class BaseScraper : IWebScraper
    {
        public async Task Scrape(PoliticianClient client, ILogger logger, CancellationToken token)
        {
            HtmlWeb website = new HtmlWeb();
            try
            {
                await Scrape(client, website, logger, token);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "test");
            }
        }

        protected abstract Task Scrape(PoliticianClient client, HtmlWeb website, ILogger logger, CancellationToken token);
    }
}