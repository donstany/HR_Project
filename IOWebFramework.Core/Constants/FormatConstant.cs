using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Core.Constants
{
    public class FormatConstant
    {
        /// <summary>
        /// Formatting of date dd.MM.yyyy - HH:mm:ss;
        /// </summary>
        public const string DateFormat = "dd.MM.yyyy - HH:mm:ss";

        /// <summary>
        /// Formatting of date dd.MM.yyyy;
        /// </summary>
        public const string NormalDateFormat = "dd.MM.yyyy";

        /// <summary>
        /// Formatting time to HH:mm
        /// </summary>
        public const string NormalTimeFormat = "HH:mm";

        /// <summary>
        /// Formatting decimal to #00.00
        /// </summary>
        public const string DecimalValueFormat = "#0.00";

        /// <summary>
        /// Formatting decimal to #00
        /// </summary>
        public const string IntValueFormat = "N";
    }
}
