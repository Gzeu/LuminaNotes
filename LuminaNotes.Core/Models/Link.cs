using SQLite;
using System;

namespace LuminaNotes.Core.Models;

/// <summary>
/// Represents a bidirectional link between two notes
/// </summary>
public class Link
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int SourceNoteId { get; set; }

    public int TargetNoteId { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    /// <summary>
    /// Optional context text around the link
    /// </summary>
    public string? Context { get; set; }
}
