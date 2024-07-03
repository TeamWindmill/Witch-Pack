using System.Text.RegularExpressions;
using UnityEngine;

namespace Tools.Helpers
{
    public static class ExtensionMethods
    {
        public static string ToLowercaseNamingConvention(this string s)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return r.Replace(s, " ").ToLower();
        }

        public static void ChangeAlpha(this ref Color color, float alphaValue)
        {
            var newColor = color;
            newColor.a = alphaValue;
            color = newColor;
        }
    }
}