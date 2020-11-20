using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper.Interfaces
{
    public interface IWebScraper
    {
        public Task Scrape(ILogger logger, CancellationToken token);
    }

    /// <summary>
    /// Anything implementing this class should not have a long running process or retrieve
    /// information from large websites the scope of ALL of the implementations needs to be below
    /// the time
    /// </summary>
    public interface I5SecondWebScraper : IWebScraper { }

    public interface I30SecondWebScraper : IWebScraper { }
}