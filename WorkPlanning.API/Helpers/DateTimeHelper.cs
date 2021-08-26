using System;

namespace WorkPlanning.API.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime StringToDate(string date) =>  DateTime.ParseExact(date, "yyyyMMdd", null);
        public static DateTime GetDateMaxTime(DateTime date) => new(date.Year, date.Month, date.Day, 23, 59, 59);
    }
}
