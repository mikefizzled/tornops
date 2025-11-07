namespace TornOps.Helpers;

using System.Net;
using System.Text.RegularExpressions;

public static class TextHelpers
{
    /// <summary>
    /// Decodes HTML entities, replaces &lt;a&gt; tags with their inner text (e.g., usernames),
    /// strips remaining tags, and cleans whitespace/punctuation.
    /// Works for "breakout", "hospitalized by", "mugged by", etc.
    /// </summary>
    public static string? CleanDetailsKeepName(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;

        var s = WebUtility.HtmlDecode(raw);

        // Replace all anchors with just their inner text (handles any attributes/quotes)
        s = Regex.Replace(
            s,
            @"<a\b[^>]*>(?<text>.*?)</a>",
            m => m.Groups["text"].Value,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        // Strip any remaining tags
        s = Regex.Replace(s, @"<[^>]+>", string.Empty);

        // Collapse whitespace and remove spaces before punctuation
        s = Regex.Replace(s, @"\s+", " ").Trim();
        s = Regex.Replace(s, @"\s+([.,!?;:])", "$1");

        return s;
    }
}
