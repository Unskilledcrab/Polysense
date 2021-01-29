using System.Collections.Generic;

namespace PS.Web.Scraper
{
    public enum TimerType
    {
        Every1Second,
        Every5Seconds,
        Every10Seconds,
        Every15Seconds,
        Every30Seconds,
        Every45Seconds,

        Every1Minute,
        Every5Minutes,
        Every10Minutes,
        Every15Minutes,
        Every30Minutes,
        Every45Minutes,

        Every1Hour,
        Every2Hours,
        Every3Hours,
        Every6Hours,
        Every12Hours,

        Every1Day,
        Every2Days,
        Every3Days,
        Every4Days,
        Every5Days,
        Every6Days,

        Every1Week,
        Every2Weeks,

        Every1Month,
        Every2Months,
        Every3Months,
        Every4Months,
        Every5Months,
        Every6Months,
    }

    public class CronExpressionHelpers
    {
        public static Dictionary<TimerType, string> CronMappings = new Dictionary<TimerType, string>
        {
            { TimerType.Every1Second, @"* * * * * *" },
            { TimerType.Every5Seconds, @"*/5 * * * * *" },
            { TimerType.Every10Seconds, @"*/10 * * * * *" },
            { TimerType.Every15Seconds, @"*/15 * * * * *" },
            { TimerType.Every30Seconds, @"*/30 * * * * *" },
            { TimerType.Every45Seconds, @"*/45 * * * * *" },

            { TimerType.Every1Minute, @"* * * * *" },
            { TimerType.Every5Minutes, @"*/5 * * * *" },
            { TimerType.Every10Minutes, @"*/10 * * * *" },
            { TimerType.Every15Minutes, @"*/15 * * * *" },
            { TimerType.Every30Minutes, @"*/30 * * * *" },
            { TimerType.Every45Minutes, @"*/45 * * * *" },

            { TimerType.Every1Hour, @"0 * * * *" },
            { TimerType.Every2Hours, @"0 */2 * * *" },
            { TimerType.Every3Hours, @"0 */3 * * *" },
            { TimerType.Every6Hours, @"0 */6 * * *" },
            { TimerType.Every12Hours, @"0 */12 * * *" },

            { TimerType.Every1Day, @"0 0 * * *" },
            { TimerType.Every2Days, @"0 0 */2 * *" },
            { TimerType.Every3Days, @"0 0 */3 * *" },
            { TimerType.Every4Days, @"0 0 */4 * *" },
            { TimerType.Every5Days, @"0 0 */5 * *" },
            { TimerType.Every6Days, @"0 0 */6 * *" },

            { TimerType.Every1Month, @"0 0 0 * *" },
            { TimerType.Every2Months, @"0 0 0 */2 *" },
            { TimerType.Every3Months, @"0 0 0 */3 *" },
            { TimerType.Every4Months, @"0 0 0 */4 *" },
            { TimerType.Every5Months, @"0 0 0 */5 *" },
            { TimerType.Every6Months, @"0 0 0 */6 *" },
        };
    }
}