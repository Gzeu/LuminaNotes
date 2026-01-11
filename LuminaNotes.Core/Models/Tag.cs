using SQLite;
using System;

namespace LuminaNotes.Core.Models;

/// <summary>
/// Represents a tag for organizing notes
/// </summary>
public class Tag
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [MaxLength(100), Unique]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Hex color code for tag (e.g., #FF5733)
    /// </summary>
    public string Color { get; set; } = "#0078D4";

    /// <summary>
    /// Parent tag ID for hierarchical tags (nullable)
    /// </summary>
    public int? ParentTagId { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    /// <summary>
    /// Number of notes using this tag (denormalized for performance)
    /// </summary>
    public int UsageCount { get; set; } = 0;
}
