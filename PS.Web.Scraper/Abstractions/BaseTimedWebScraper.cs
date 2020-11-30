using Microsoft.Extensions.Logging;
using PS.Shared.HttpClients;
using PS.Web.Scraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper.Abstractions
{
    public abstract class BaseTimedWebScaper<T> : CronJobService
    {
        protected PoliticianClient politicianClient;
        private readonly ILogger<BaseTimedWebScaper<T>> _logger;
        private IList<IWebScraper> _webScrapers;

        public BaseTimedWebScaper(PoliticianClient client, IScheduleConfig<T> config, ILogger<BaseTimedWebScaper<T>> logger) : base(config.CronExpression, TimeZoneInfo.Local)
        {
            _webScrapers = config.WebScrapers;
            _logger = logger;
            politicianClient = client;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            foreach (var scraper in _webScrapers)
            {
                if (!cancellationToken.IsCancellationRequested)
                    await scraper.Scrape(politicianClient, _logger, cancellationToken);
            }
        }
    }
}