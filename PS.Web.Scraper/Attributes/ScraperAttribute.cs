using System;

namespace PS.Web.Scraper.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScraperAttribute : Attribute
    {
        public ScraperAttribute(TimerType timer, bool isProductionReady = false)
        {
            Timer = timer;
            IsProductionReady = isProductionReady;
        }

        public bool IsProductionReady { get; set; } = false;
        public TimerType Timer { get; set; }
    }
}