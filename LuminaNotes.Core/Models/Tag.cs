using SQLite;
using System;

namespace LuminaNotes.Core.Models;

/// <summary>
/// Represents a tag for organizing notes
/// </summary>
public class Tag
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Hex color for UI display (e.g., "#FF5733")
    /// </summary>
    public string Color { get; set; } = "#0078D4";

    public DateTime Created { get; set; } = DateTime.Now;

    /// <summary>
    /// Parent tag ID for hierarchical tags (null if root)
    /// </summary>
    public int? ParentId { get; set; }
}
