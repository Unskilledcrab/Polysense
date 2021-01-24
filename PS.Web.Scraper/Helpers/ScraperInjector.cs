using Microsoft.Extensions.DependencyInjection;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Attributes;
using PS.Web.Scraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PS.Web.Scraper.Helpers
{
    public static class ScraperInjector
    {
        public static IList<IWebScraper> GetScrapers(Type type, IServiceCollection services)
        {
            var webScrapers = new List<IWebScraper>();
            var allWebScrapersTypes = Assembly.GetAssembly(type).GetTypes()
                .Where(t => type.IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var scraperType in allWebScrapersTypes)
            {
                BaseScraper scaper = Activator.CreateInstance(scraperType) as BaseScraper;
                webScrapers.Add(scaper);
            }
            return webScrapers;
        }

        public static void BuildScrapers()
        {
            var allScrapers = Assembly.GetAssembly(typeof(BaseScraper)).GetTypes()
                .Where(t => typeof(BaseScraper).IsAssignableFrom(t) && t.IsAbstract == false);

            var timers = new List<string>();
            foreach (var scraperType in allScrapers)
            {
                ScraperAttribute attribute = (ScraperAttribute)Attribute.GetCustomAttribute(scraperType, typeof(ScraperAttribute));
                if (attribute != null)
                {
                    var expression = CronExpressionHelpers.CronMappings.GetValueOrDefault(attribute.Timer);
                    if (!timers.Contains(expression))
                        timers.Add(expression);
                }
            }
        }
    }
}