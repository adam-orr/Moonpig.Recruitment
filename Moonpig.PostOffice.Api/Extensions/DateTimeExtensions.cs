using System;

namespace Moonpig.PostOffice.Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetNextWorkDay(this DateTime date)
        {
            int daysToAdd = ((int)DayOfWeek.Monday - (int)date.DayOfWeek + 7) % 7;
            return date.AddDays(daysToAdd);
        }

        public static DateTime AddWorkdays(this DateTime originalDate, int workDays)
        {
            DateTime newDate = originalDate;
            var workDaysRemaining = workDays;

            while (workDaysRemaining > 0)
            {
                newDate = newDate.AddDays(1);
                if (newDate.DayOfWeek != DayOfWeek.Saturday && newDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    //Only subtracts remaining work days when the new date is a working day (Mon - Fri)
                    workDaysRemaining -= 1;
                }
            }

            return newDate;
        }
    }
}
