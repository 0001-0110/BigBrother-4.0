using System.Text.RegularExpressions;

namespace BigBrother.Utilities.Extensions
{
    internal static class StringExtensions
    {
        public static string Indent(this string text, ushort indents, ushort indentSize = 4)
        {
            return new Regex("^").Replace(text, new string(' ', indentSize * indents));
        }
    }
}
