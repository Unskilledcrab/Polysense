using HtmlAgilityPack;
using PS.Web.Scraper.Interfaces;
using System;
using System.Threading.Tasks;

namespace PS.Web.Scraper.Abstractions
{
    abstract public class BaseScraper : IWebScraper
    {
        public async Task Scrap()
        {
            HtmlWeb website = new HtmlWeb();
            try
            {
                await Scrap(website);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected abstract Task Scrap(HtmlWeb website);
    }
}