using Microsoft.Extensions.Logging;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Extensions;
using PS.Web.Scraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper
{
    public class WebScaper5Seconds : CronJobService
    {
        private readonly ILogger<WebScaper5Seconds> _logger;
        private IList<IWebScraper> webScrapers;

        public WebScaper5Seconds(IScheduleConfig<WebScaper5Seconds> config, ILogger<WebScaper5Seconds> logger) : base(config.CronExpression, TimeZoneInfo.Local)
        {
            _logger = logger;
            webScrapers = ScraperExtensions.Get5SecondScapers();
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            foreach (var scraper in webScrapers)
            {
                if (!cancellationToken.IsCancellationRequested)
                    await scraper.Scrap();
            }
        }
    }
}