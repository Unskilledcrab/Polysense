using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PS.Shared.HttpClients;
using PS.Shared.Models;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Scraper.Manual
{
    public class ManualBillScraper : BaseScraper, ITestWebScraper
    {
        public void GetEnactedLaws()
        {
            var EnactedLaws = new List<Bill>();
            var sourceUrl = "https://www.govtrack.us/congress/bills/browse?status=28,29,32,33&sort=-current_status_date";
            IWebDriver driver = new ChromeDriver();
            driver.Url = sourceUrl;
            //Get Bills from list
            var results = driver.FindElements(By.XPath("//div[@class='col-xs-10 col-md-11']"));
            foreach (var element in results)
            {
                //Get bill detials
                var url = element.FindElement(By.TagName("a")).GetAttribute("href");
                driver.Url = url;
                try
                {
                    var _name = driver.FindElement(By.ClassName("h1-multiline")).FindElement(By.TagName("h1")).Text;
                    var _sponsor = driver.FindElement(By.ClassName("name")).Text;
                    //Get statuses
                    var statusTable = driver.FindElement(By.Id("status-row-grid"));
                    var statustable2 = statusTable.FindElements(By.XPath("//tr[@class='status-item  ']"));
                    foreach (var status in statustable2)
                    {
                        var newStatus = new
                        {
                            Bill = _name,
                            Status = status.FindElement(By.ClassName("status-label")).Text,
                            TransitionDate = status.FindElement(By.ClassName("nowrap")).Text,
                        };
                    }

                    //get cosponsors
                    driver.Url = url + "/details";
                    var _cosponsorsTable = driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));

                    foreach (var sponsor in _cosponsorsTable)
                    {
                        var name = sponsor.FindElement(By.TagName("a")).Text;
                    }
                    var meow = (from a in _cosponsorsTable
                                select a.FindElement(By.TagName("a")).Text).ToList();
                    // Try to upload the new scraped text data
                }
                catch (HttpRequestException ex)
                {
                    // If it is a conflict (already in the database) continue, otherwise throw the error.
                    if (ex.StatusCode != System.Net.HttpStatusCode.Conflict)
                        throw;
                }
            }
        }

        protected override Task Scrape(ScraperTextClient client, HtmlWeb website, ILogger logger, CancellationToken token)
        {
            var watch = Stopwatch.StartNew();
            logger.LogInformation("About to scrape GovTrack");
            GetLegislators();
            GetEnactedLaws();
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            logger.LogInformation($"Scrapped GovTrack in {elapsedTime} ms");
            return Task.CompletedTask;
        }

        private void GetLegislators()
        {
            var Representatives = new List<Politician>();
            var Senators = new List<Politician>();
            IWebDriver driver = new ChromeDriver();
            try
            {
                var HORUrl = "https://www.govtrack.us/congress/members/current#current_role_title=Representative";
                driver.Url = HORUrl;
                var results = driver.FindElement(By.ClassName("results")).FindElements(By.TagName("a"));
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
            //House of representatives
        }
    }
}