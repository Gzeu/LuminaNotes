using SQLite;
using System;

namespace LuminaNotes.Core.Models;

/// <summary>
/// Represents a note in the system with Markdown content support
/// </summary>
public class Note
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Markdown-formatted content
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// JSON-serialized list of tag IDs
    /// </summary>
    public string Tags { get; set; } = "[]";

    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime Updated { get; set; } = DateTime.Now;

    /// <summary>
    /// Indicates if this is a daily note
    /// </summary>
    public bool IsDailyNote { get; set; } = false;

    /// <summary>
    /// Optional date for daily notes
    /// </summary>
    public DateTime? DailyNoteDate { get; set; }

    /// <summary>
    /// Indicates if content is encrypted
    /// </summary>
    public bool IsEncrypted { get; set; } = false;
}
