using System;

namespace ResourceServer.Extensions
{
    public static class DateTimeExtensions
    {
        public static int AgeInYears(this DateTime birthday)
        {
            var age = DateTime.UtcNow.Year - birthday.Year;
            if (DateTime.UtcNow.DayOfYear < birthday.DayOfYear)
            {
                age--;
            }

            return age;
        }
    }
}