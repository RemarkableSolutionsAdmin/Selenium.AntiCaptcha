﻿using System.Text.RegularExpressions;

namespace Selenium.AntiCaptcha.Internal.Extensions;

internal static class StringExtensions
{
    private const RegexOptions CaseInsensitiveOptions = RegexOptions.IgnoreCase;
    
    public static bool DoesContainRegex(this string text, params string[] regexPatterns)
    {
        foreach (var pattern in regexPatterns)
        {
            if (new Regex(pattern).Match(text).Success)
                return true;
        }

        return false;
    }

    public static Match? GetFirstRegexThatFits(this string text, bool ignoreCase = false, params string[] regexPatterns)
    {
        return (regexPatterns
            .Select(pattern => new { pattern, regOptions = CaseInsensitiveOptions })
            .Select(@t => ignoreCase ? new Regex(@t.pattern, @t.regOptions) : new Regex(@t.pattern))
            .Select(regex => regex.Match(text)))
            .FirstOrDefault(match => match.Success);
    }
}