using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Shared.Common
{
    public interface IExperienceCalculator
    {
        public (int Year, int Month, int Day) SplitDate(int? totalDays = null);
        public int AggregateDateTokens(int? year, int? month, int? day);
    }
}
