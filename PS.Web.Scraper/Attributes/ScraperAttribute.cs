using System;

namespace PS.Web.Scraper.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScraperAttribute : Attribute
    {
        private bool isProductionReady = false;

        public ScraperAttribute(TimerType timer, bool isProductionReady = false)
        {
            Timer = timer;
            this.isProductionReady = isProductionReady;
        }

        public TimerType Timer { get; set; }
    }
}