using System.Threading.Tasks;

namespace PS.Web.Scraper.Interfaces
{
    public interface IWebScraper
    {
        public Task Scrap();
    }

    public interface I5SecondWebScraper : IWebScraper { }
}