using System;
using System.Collections.Generic;
using System.Text;

namespace Infra
{
    public static class DateUtil
    {
        public static double CalcDiffInMinutes(this DateTime startDate, DateTime endDate)
        {
            TimeSpan diff = endDate-startDate;
            return Math.Floor(diff.TotalMinutes);
        }

        public static DateTime GetNextDay(this DateTime date)
        {
            var tomorrow = date.AddDays(1);
            var ts = new TimeSpan(0, 0, 0);
            return tomorrow.Date + ts;
        }
    }
}
