using Markdig;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LuminaNotes.Core.Utilities;

/// <summary>
/// Utilities for Markdown parsing and wiki-link extraction
/// </summary>
public static class MarkdownHelper
{
    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    /// <summary>
    /// Converts Markdown to HTML
    /// </summary>
    public static string ToHtml(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return string.Empty;

        return Markdown.ToHtml(markdown, Pipeline);
    }

    /// <summary>
    /// Extracts wiki-style links [[Note Title]] from content
    /// </summary>
    public static List<string> ExtractWikiLinks(string content)
    {
        var links = new List<string>();
        if (string.IsNullOrEmpty(content))
            return links;

        // Regex pattern for [[wiki links]]
        var pattern = @"\[\[([^\]]+)\]\]";
        var matches = Regex.Matches(content, pattern);

        foreach (Match match in matches)
        {
            if (match.Groups.Count > 1)
            {
                links.Add(match.Groups[1].Value.Trim());
            }
        }

        return links;
    }

    /// <summary>
    /// Converts wiki links to clickable HTML links
    /// </summary>
    public static string RenderWikiLinks(string content)
    {
        if (string.IsNullOrEmpty(content))
            return content;

        var pattern = @"\[\[([^\]]+)\]\]";
        return Regex.Replace(content, pattern, match =>
        {
            var linkText = match.Groups[1].Value;
            return $"<a href='#note:{linkText}' class='wiki-link'>{linkText}</a>";
        });
    }

    /// <summary>
    /// Extracts plain text from Markdown (strips formatting)
    /// </summary>
    public static string ToPlainText(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return string.Empty;

        // Remove Markdown syntax
        var plain = Regex.Replace(markdown, @"\*\*(.+?)\*\*", "$1"); // Bold
        plain = Regex.Replace(plain, @"\*(.+?)\*", "$1"); // Italic
        plain = Regex.Replace(plain, @"__(.+?)__", "$1"); // Bold alt
        plain = Regex.Replace(plain, @"_(.+?)_", "$1"); // Italic alt
        plain = Regex.Replace(plain, @"^#+\s+", "", RegexOptions.Multiline); // Headers
        plain = Regex.Replace(plain, @"\[([^\]]+)\]\([^)]+\)", "$1"); // Links
        plain = Regex.Replace(plain, @"```[^`]*```", "", RegexOptions.Singleline); // Code blocks
        plain = Regex.Replace(plain, @"`([^`]+)`", "$1"); // Inline code

        return plain.Trim();
    }
}
