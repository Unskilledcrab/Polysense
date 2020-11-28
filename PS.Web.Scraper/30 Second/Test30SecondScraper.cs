using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PS.Shared.Clients;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper._30_Second
{
    public class Test30SecondScraper : BaseScraper, ITestWebScraper
    {
        private readonly PoliticianClient client;

        protected override async Task Scrape(HtmlWeb website, ILogger logger, CancellationToken token)
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