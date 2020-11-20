using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PS.Web.Scraper.Helpers
{
    public static class ScraperInjector
    {
        public static IList<IWebScraper> GetScrapers(Type type)
        {
            var webScrapers = new List<IWebScraper>();
            var allWebScrapersTypes = Assembly.GetAssembly(type).GetTypes()
                .Where(t => type.IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var scaperType in allWebScrapersTypes)
            {
                BaseScraper scaper = Activator.CreateInstance(scaperType) as BaseScraper;
                webScrapers.Add(scaper);
            }
            return webScrapers;
        }
    }
}