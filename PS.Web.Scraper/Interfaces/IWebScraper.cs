using Microsoft.Extensions.Logging;
using PS.Shared.HttpClients;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper.Interfaces
{
    public interface IWebScraper
    {
        public Task Scrape(PoliticianClient client, ILogger logger, CancellationToken token);
    }

    public interface ITestWebScraper : IWebScraper { }

    public interface I5SecondWebScraper : IWebScraper { }

    public interface I30SecondWebScraper : IWebScraper { }

    public interface I1MinuteWebScraper : IWebScraper { }
}