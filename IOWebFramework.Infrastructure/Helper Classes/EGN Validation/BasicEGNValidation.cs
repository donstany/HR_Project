using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IOWebFramework.Infrastructure.Helper_Classes.EGN_Validation
{
    public class BasicEGNValidation : EGNValidation
    {
        private const string cErrorMessage = "Невалидно ЕГН!";
        private const string cErrorMessageEn = "Invalid EGN!";
        private int[] egnWeights = new int[] { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
        private string egn;
        protected int year;
        protected int month;
        protected int day;
        public BasicEGNValidation(string egn)
        {
            this.ErrorMessage = GetCulturalInfoMessage();
            this.egn = egn;
        }



        public override string GetMessage()
        {
            if (IsValid)
            {
                ErrorMessage = string.Empty;
            }
            else
            {
                ErrorMessage = cErrorMessage;
            }
            return ErrorMessage;
        }



        public override bool Validate()
        {
            IsValid = false;
            try
            {
                if (egn.Length != 10)
                {
                    return IsValid;
                }
                if (!RegexEgnCheck(egn))
                {
                    return IsValid;
                }



                year = int.Parse(egn.Substring(0, 2));
                month = int.Parse(egn.Substring(2, 2));
                day = int.Parse(egn.Substring(4, 2));



                SetDateByMonth(ref year, ref month, ref day);
                if (!CheckDate(year, month, day))
                {
                    return IsValid;
                }
                if (!CheckSum(int.Parse(egn.Substring(9, 1))))
                {
                    return IsValid;
                }
                IsValid = true;
            }
            catch (Exception)
            {
                IsValid = false;
            }
            return IsValid;
        }
        private bool CheckSum(int check)
        {
            bool result = false;
            try
            {
                int egnSum = 0;
                int validCheckSum = 0;
                for (int index = 0; index < 9; index++)
                {
                    egnSum += (int.Parse(egn.Substring(index, 1))) * egnWeights[index];
                }
                validCheckSum = egnSum % 11;
                if (validCheckSum == 10)
                {
                    validCheckSum = 0;
                }
                if (check == validCheckSum)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        private bool CheckDate(int year, int month, int day)
        {
            bool result = false;
            try
            {
                if (year < 1800)
                {
                    return result;
                }
                DateTime dt = new DateTime(year, month, day);
                result = true;
                EGNDate = dt;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }
            return result;
        }
        private void SetDateByMonth(ref int year, ref int month, ref int day)
        {
            if (month > 40)
            {
                year += 2000;
                month -= 40;
            }
            else
            {
                if (month > 20)
                {
                    year += 1800;
                    month -= 20;
                }
                else
                {
                    year += 1900;
                }



            }
        }
        /// <summary>
        /// Language URL params
        /// </summary>
        public struct Regional_Url
        {
            //public const string UrlParam = "lng";
            public const string UrlLangEN = "en";
            public const string UrlLangBG = "bg";
        }
        private string GetCulturalInfoMessage()
        {
            string result = cErrorMessage;
            //string result = string.Empty;
            //switch (Thread.CurrentThread.CurrentUICulture.DisplayName.Substring(0, 2).ToLower())
            //{
            //    case Regional_Url.UrlLangBG:
            //        result = cErrorMessage; break;
            //    case Regional_Url.UrlLangEN:
            //        result = cErrorMessageEn; break;
            //}
            return result;
        }
        private bool RegexEgnCheck(string egn)
        {
            Regex r = new Regex("[0-9]{2}[0,1,2,3,4,5][0-9][0-9]{2}[0-9]{4}");
            if (r.IsMatch(egn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
