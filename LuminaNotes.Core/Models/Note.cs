using SQLite;
using System;

namespace LuminaNotes.Core.Models;

/// <summary>
/// Represents a note in the system with markdown content, tags, and metadata
/// </summary>
public class Note
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [MaxLength(500)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Markdown-formatted content
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// JSON serialized list of tag names
    /// </summary>
    public string Tags { get; set; } = "[]";

    /// <summary>
    /// Comma-separated list of linked note IDs
    /// </summary>
    public string LinkedNoteIds { get; set; } = string.Empty;

    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime Updated { get; set; } = DateTime.Now;

    /// <summary>
    /// Flag for daily notes (auto-created)
    /// </summary>
    public bool IsDailyNote { get; set; } = false;

    /// <summary>
    /// Date for daily note (YYYY-MM-DD format)
    /// </summary>
    public string? DailyNoteDate { get; set; }

    /// <summary>
    /// Is note encrypted with AES-256
    /// </summary>
    public bool IsEncrypted { get; set; } = false;

    /// <summary>
    /// Favorite/pinned status
    /// </summary>
    public bool IsPinned { get; set; } = false;
}
