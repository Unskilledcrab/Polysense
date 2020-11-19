namespace PS.Web.Scraper.Interfaces
{
    public interface IScheduleConfig<T>
    {
        string CronExpression { get; set; }
    }

    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
    }
}