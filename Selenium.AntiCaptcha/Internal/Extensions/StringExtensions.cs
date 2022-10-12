using System.Text.RegularExpressions;

namespace Selenium.AntiCaptcha.Internal.Extensions;

internal static class StringExtensions
{
    public static bool DoesContainRegex(this string text, params string[] regexes)
    {
        foreach (var regex in regexes)
        {
            if (new Regex(regex).Match(text).Success)
                return true;
        }

        return false;
    }
}