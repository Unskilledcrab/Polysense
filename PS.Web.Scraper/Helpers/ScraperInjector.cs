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

            var timers = new List<ScraperHolder>();
            foreach (var scraperType in allScrapers)
            {
                ScraperAttribute attribute = (ScraperAttribute)Attribute.GetCustomAttribute(scraperType, typeof(ScraperAttribute));
                if (attribute != null)
                {
                    BaseScraper scaper = Activator.CreateInstance(scraperType) as BaseScraper;

                    string expression = @"*/5 * * * * *";
                    if (attribute.IsProductionReady)
                        expression = CronExpressionHelpers.CronMappings.GetValueOrDefault(attribute.Timer);

                    if (!timers.Any(t => t.CronExpression == expression))
                    {
                        timers.Add(new ScraperHolder { CronExpression = expression, Scrapers = new List<BaseScraper> { scaper } });
                    }
                    else
                    {
                        var holder = timers.First(t => t.CronExpression == expression);
                        holder.Scrapers.Add(scaper);
                    }
                }
            }
        }
    }

    public class ScraperHolder
    {
        public string CronExpression { get; set; }
        public List<BaseScraper> Scrapers { get; set; } = new List<BaseScraper>();
    }
}