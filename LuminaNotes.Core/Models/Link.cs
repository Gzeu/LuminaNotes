using SQLite;
using System;

namespace LuminaNotes.Core.Models;

/// <summary>
/// Represents a bidirectional link between two notes
/// </summary>
public class Link
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    /// <summary>
    /// Source note ID
    /// </summary>
    [Indexed]
    public int SourceNoteId { get; set; }

    /// <summary>
    /// Target note ID (the linked note)
    /// </summary>
    [Indexed]
    public int TargetNoteId { get; set; }

    /// <summary>
    /// Optional link type (e.g., "references", "expands", "contradicts")
    /// </summary>
    public string? LinkType { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    /// <summary>
    /// Context text around the link (for hover previews)
    /// </summary>
    public string? Context { get; set; }
}
