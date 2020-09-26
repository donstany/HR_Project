namespace IOWebFrameworkApplicationService.Extentions
{
    public static class StringExtention
    {
        public static string CutLastChar(this string str)
        {
            return str.Substring(0, str.Length - 1).Trim();
        }
    }
}