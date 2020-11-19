using Microsoft.Extensions.DependencyInjection;
using PS.Web.Scraper.Abstractions;
using PS.Web.Scraper.Interfaces;
using System;

namespace PS.Web.Scraper.Extensions
{
    public static class ScheduledServiceExtensions
    {
        public static IServiceCollection AddWebScrapers(this IServiceCollection services)
        {
            services.AddCronJob<WebScaper5Seconds>(c =>
            {
                c.CronExpression = @"*/5 * * * * *";
            });
            return services;
        }

        public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new ScheduleConfig<T>();
            options.Invoke(config);
            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}