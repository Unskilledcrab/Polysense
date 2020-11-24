﻿using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper._30_Second
{
    public class Test30SecondScraper : BaseScraper, ITestWebScraper
    {
        protected override Task Scrape(HtmlWeb website, ILogger logger, CancellationToken token)
        {
            logger.LogInformation("<<<<<<<<<<<<<<<------------------- TEST 30 SECOND SCRAPE ---------------------->>>>>>>>>>>>>>>");
            return Task.CompletedTask;
        }
    }
}