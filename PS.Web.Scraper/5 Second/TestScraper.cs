using HtmlAgilityPack;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System.Threading.Tasks;

namespace PS.Web.Scraper._5_Second
{
    public class TestScraper : BaseScraper, I5SecondWebScraper
    {
        public override async Task Scrap(HtmlWeb website)
        {
            var doc = await website.LoadFromWebAsync("https://www.ubunlimited.com");
        }
    }
}