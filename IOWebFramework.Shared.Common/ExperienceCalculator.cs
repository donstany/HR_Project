using System;

namespace IOWebFramework.Shared.Common
{
    public class ExperienceCalculator : IExperienceCalculator
    {
        //калкулатор трудов стаж https://activ2009.com/stsb.html
        private const int _standardYear = 360;
        private const int _standardMonth = 30;

        public (int Year, int Month, int Day) SplitDate(int? totalDays = null)
        {
            int year = 0;
            int month = 0;
            int day = 0;
            if (!totalDays.HasValue || totalDays == 0)
            {
                return (year, month, day);
            }

            year = (int)Math.Floor((decimal)(totalDays.Value / _standardYear));
            var wholeYear = year * _standardYear;
            var monthsRemaining = totalDays.Value - wholeYear;

            month = (int)Math.Floor((decimal)(monthsRemaining / _standardMonth));
            var wholeMonths = month * _standardMonth;

            day = monthsRemaining - wholeMonths;
            return (year, month, day);
        }

        public int AggregateDateTokens(int? year, int? month, int? day)
        {
            year ??= 0;
            month ??= 0;
            day ??= 0;

            int result = (year.Value * _standardYear) + (month.Value * _standardMonth) + day.Value;

            return result;
        }
    }
}
