using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper.Interfaces
{
    public interface IWebScraper
    {
        public Task Scrape(ILogger logger, CancellationToken token);
    }

    public interface I5SecondWebScraper : IWebScraper { }

    public interface I30SecondWebScraper : IWebScraper { }

    public interface I1MinuteWebScraper : IWebScraper { }
}