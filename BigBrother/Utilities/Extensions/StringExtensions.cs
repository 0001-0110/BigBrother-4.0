using System.Text.RegularExpressions;

namespace BigBrother.Utilities.Extensions
{
    internal static class StringExtensions
    {
        public static string Indent(this string str, ushort indents, ushort indentSize = 4)
        {
            return new Regex("^", RegexOptions.Multiline).Replace(str, new string(' ', indents * indentSize));
        }

        public static string Remove(this string str, string toRemove)
        {
            return str.Replace(toRemove, string.Empty);
        }
    }
}
