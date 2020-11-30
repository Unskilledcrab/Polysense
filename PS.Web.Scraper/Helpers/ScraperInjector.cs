using Microsoft.Extensions.DependencyInjection;
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
        public static IList<IWebScraper> GetScrapers(Type type, IServiceCollection services)
        {
            var webScrapers = new List<IWebScraper>();
            var allWebScrapersTypes = Assembly.GetAssembly(type).GetTypes()
                .Where(t => type.IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var scraperType in allWebScrapersTypes)
            {
                //var constructors = scraperType.GetConstructors();
                //var firstConstructor = constructors.FirstOrDefault();
                //if (firstConstructor != null)
                //{
                //    var constructorParameters = firstConstructor.GetParameters();
                //    if (constructorParameters != null && constructorParameters.Any())
                //    {
                //        var objectList = new List<object>();

                //        foreach (var constructorParameter in constructorParameters)
                //        {
                //            var parameterType = constructorParameter.ParameterType;
                //            //var instance = services.
                //        }
                //    }
                //}
                BaseScraper scaper = Activator.CreateInstance(scraperType) as BaseScraper;
                webScrapers.Add(scaper);
            }
            return webScrapers;
        }
    }
}