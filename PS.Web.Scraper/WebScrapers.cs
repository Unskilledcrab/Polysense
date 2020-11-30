using Microsoft.Extensions.Logging;
using PS.Shared.HttpClients;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;

namespace PS.Web.Scraper
{
    public class TestWebScraper : BaseTimedWebScaper<TestWebScraper>
    {
        public TestWebScraper(PoliticianClient client, IScheduleConfig<TestWebScraper> config, ILogger<BaseTimedWebScaper<TestWebScraper>> logger) : base(client, config, logger)
        {
        }
    }

    public class WebScaper5Seconds : BaseTimedWebScaper<WebScaper5Seconds>
    {
        public WebScaper5Seconds(PoliticianClient client, IScheduleConfig<WebScaper5Seconds> config, ILogger<BaseTimedWebScaper<WebScaper5Seconds>> logger) : base(client, config, logger)
        {
        }
    }

    public class WebScaper30Seconds : BaseTimedWebScaper<WebScaper30Seconds>
    {
        public WebScaper30Seconds(PoliticianClient client, IScheduleConfig<WebScaper30Seconds> config, ILogger<BaseTimedWebScaper<WebScaper30Seconds>> logger) : base(client, config, logger)
        {
        }
    }

    public class WebScaper1Minute : BaseTimedWebScaper<WebScaper1Minute>
    {
        public WebScaper1Minute(PoliticianClient client, IScheduleConfig<WebScaper1Minute> config, ILogger<BaseTimedWebScaper<WebScaper1Minute>> logger) : base(client, config, logger)
        {
        }
    }
}