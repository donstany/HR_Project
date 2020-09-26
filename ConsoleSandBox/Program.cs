using System;
using System.Linq;
using IOWebFramework.Shared.Common;

namespace ConsoleSandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = ActiveDirectoryHelper.LdapLogin("ststanev", "fake!!!g");
            //var b = ActiveDirectoryHelper.GetUserInfo("ststanev", "fake!!!");
            //var dtoResult = ActiveDirectoryHelper.ImportAllUserDetails();
            var resultUserInfo = ActiveDirectoryHelper.ImportUsersDetailsByEmail(" a.ivanova@is-bg.net").First();
            
            var resultAllUsersInfo = ActiveDirectoryHelper.ImportUsersDetailsByEmail(enabeledActiveAccountInAD:true);
            ;
            //var emailInAD = "st.stanev@is-bg.net";
            //var dtoResultByEmail = ActiveDirectoryHelper.ImportUserDetailsByEmail(emailInAD);
         
            var b = (new ExperienceCalculator()).SplitDate(800);
            Console.WriteLine(b.Year);
            Console.WriteLine(b.Month);
            Console.WriteLine(b.Day);
        }
    }
}
