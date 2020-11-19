using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PS.Web.Scraper.Extensions
{
    public static class ScraperExtensions
    {
        public static IList<IWebScraper> Get5SecondScapers()
        {
            var webScrapers = new List<IWebScraper>();
            var allWebScrapersTypes = Assembly.GetAssembly(typeof(I5SecondWebScraper)).GetTypes()
                .Where(t => typeof(I5SecondWebScraper).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var scaperType in allWebScrapersTypes)
            {
                BaseScraper scaper = Activator.CreateInstance(scaperType) as BaseScraper;
                webScrapers.Add(scaper);
            }
            return webScrapers;
        }
    }
}