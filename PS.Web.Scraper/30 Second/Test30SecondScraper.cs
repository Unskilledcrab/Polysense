using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PS.Shared.HttpClients;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper._30_Second
{
    public class Test30SecondScraper : BaseScraper, ITestWebScraper
    {
        protected override async Task Scrape(PoliticianClient client, HtmlWeb website, ILogger logger, CancellationToken token)
        {
            logger.LogInformation("<<<<<<<<<<<<<<<------------------- TEST 30 SECOND SCRAPE ---------------------->>>>>>>>>>>>>>>");

            //await client.SetPolitician(new Shared.Models.Politician
            //{
            //    FirstName = "Mr. Poopie",
            //    LastName = "Pants",
            //    CurrentOffice = Shared.Models.PoliticalOfficeType.AttorneyGeneral,
            //    BirthDateTime = DateTime.Now,
            //});
        }
    }
}