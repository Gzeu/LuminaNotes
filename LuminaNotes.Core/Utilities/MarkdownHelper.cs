using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LuminaNotes.Core.Utilities;

/// <summary>
/// Helper for parsing and processing Markdown content
/// </summary>
public static class MarkdownHelper
{
    private static readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    private static readonly Regex WikiLinkRegex = new(@"\[\[([^\]]+)\]\]", RegexOptions.Compiled);
    private static readonly Regex TagRegex = new(@"#([\w-]+)", RegexOptions.Compiled);

    /// <summary>
    /// Convert Markdown to HTML
    /// </summary>
    public static string ToHtml(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return string.Empty;

        return Markdown.ToHtml(markdown, _pipeline);
    }

    /// <summary>
    /// Convert Markdown to plain text (strip formatting)
    /// </summary>
    public static string ToPlainText(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return string.Empty;

        return Markdown.ToPlainText(markdown, _pipeline);
    }

    /// <summary>
    /// Extract wiki-style links from content [[Note Title]]
    /// </summary>
    public static List<string> ExtractWikiLinks(string content)
    {
        if (string.IsNullOrEmpty(content))
            return new List<string>();

        var matches = WikiLinkRegex.Matches(content);
        return matches.Select(m => m.Groups[1].Value.Trim()).Distinct().ToList();
    }

    /// <summary>
    /// Extract hashtags from content
    /// </summary>
    public static List<string> ExtractTags(string content)
    {
        if (string.IsNullOrEmpty(content))
            return new List<string>();

        var matches = TagRegex.Matches(content);
        return matches.Select(m => m.Groups[1].Value.ToLower()).Distinct().ToList();
    }

    /// <summary>
    /// Replace wiki links with HTML links
    /// </summary>
    public static string ReplaceWikiLinksWithHtml(string content, Func<string, string> linkResolver)
    {
        if (string.IsNullOrEmpty(content))
            return content;

        return WikiLinkRegex.Replace(content, match =>
        {
            var linkText = match.Groups[1].Value.Trim();
            var url = linkResolver(linkText);
            return $"<a href=\"{url}\" class=\"wiki-link\">{linkText}</a>";
        });
    }

    /// <summary>
    /// Get word count from markdown
    /// </summary>
    public static int GetWordCount(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return 0;

        var plainText = ToPlainText(markdown);
        var words = plainText.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }

    /// <summary>
    /// Extract first heading (H1) from markdown
    /// </summary>
    public static string? ExtractFirstHeading(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return null;

        var headingMatch = Regex.Match(markdown, @"^#\s+(.+)$", RegexOptions.Multiline);
        return headingMatch.Success ? headingMatch.Groups[1].Value.Trim() : null;
    }

    /// <summary>
    /// Generate a preview/excerpt from markdown (first N words)
    /// </summary>
    public static string GeneratePreview(string markdown, int maxWords = 50)
    {
        if (string.IsNullOrEmpty(markdown))
            return string.Empty;

        var plainText = ToPlainText(markdown);
        var words = plainText.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        
        if (words.Length <= maxWords)
            return plainText;

        return string.Join(" ", words.Take(maxWords)) + "...";
    }

    /// <summary>
    /// Sanitize filename from note title
    /// </summary>
    public static string SanitizeFileName(string title)
    {
        if (string.IsNullOrEmpty(title))
            return "untitled";

        var invalid = Path.GetInvalidFileNameChars();
        var sanitized = new string(title.Where(c => !invalid.Contains(c)).ToArray());
        return string.IsNullOrEmpty(sanitized) ? "untitled" : sanitized;
    }
}
