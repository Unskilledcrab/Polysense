using Microsoft.Extensions.Logging;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper
{
    public class WebScaper5Seconds : BaseTimedWebScaper<WebScaper5Seconds>
    {
        public WebScaper5Seconds(IScheduleConfig<WebScaper5Seconds> config, ILogger<BaseTimedWebScaper<WebScaper5Seconds>> logger) : base(config, logger)
        {
        }
    }

    public class WebScaper30Seconds : BaseTimedWebScaper<WebScaper30Seconds>
    {
        public WebScaper30Seconds(IScheduleConfig<WebScaper30Seconds> config, ILogger<BaseTimedWebScaper<WebScaper30Seconds>> logger) : base(config, logger)
        {
        }
    }

    public abstract class BaseTimedWebScaper<T> : CronJobService
    {
        private readonly ILogger<BaseTimedWebScaper<T>> _logger;
        private IList<IWebScraper> _webScrapers;

        public BaseTimedWebScaper(IScheduleConfig<T> config, ILogger<BaseTimedWebScaper<T>> logger) : base(config.CronExpression, TimeZoneInfo.Local)
        {
            _webScrapers = config.WebScrapers;
            _logger = logger;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            foreach (var scraper in _webScrapers)
            {
                if (!cancellationToken.IsCancellationRequested)
                    await scraper.Scrape(_logger, cancellationToken);
            }
        }
    }
}