namespace BigBrother.Utilities.Extensions
{
    internal static class StringExtensions
    {
        public static string Remove(this string str, string toRemove)
        {
            return str.Replace(toRemove, string.Empty);
        }
    }
}
