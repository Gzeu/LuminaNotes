using LuminaNotes.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuminaNotes.Core.Services;

/// <summary>
/// Handles graph visualization logic for note connections
/// </summary>
public class GraphService
{
    private readonly DatabaseService _databaseService;
    private readonly NoteService _noteService;

    public GraphService(DatabaseService databaseService, NoteService noteService)
    {
        _databaseService = databaseService;
        _noteService = noteService;
    }

    /// <summary>
    /// Get all links in the system
    /// </summary>
    public async Task<List<Link>> GetAllLinksAsync()
    {
        var db = _databaseService.GetConnection();
        return await db.Table<Link>().ToListAsync();
    }

    /// <summary>
    /// Get links for a specific note (both incoming and outgoing)
    /// </summary>
    public async Task<(List<Link> outgoing, List<Link> incoming)> GetNoteLinksAsync(int noteId)
    {
        var db = _databaseService.GetConnection();
        
        var outgoing = await db.Table<Link>()
            .Where(l => l.SourceNoteId == noteId)
            .ToListAsync();
        
        var incoming = await db.Table<Link>()
            .Where(l => l.TargetNoteId == noteId)
            .ToListAsync();
        
        return (outgoing, incoming);
    }

    /// <summary>
    /// Create a bidirectional link between two notes
    /// </summary>
    public async Task<Link> CreateLinkAsync(int sourceNoteId, int targetNoteId, string? linkType = null, string? context = null)
    {
        var db = _databaseService.GetConnection();
        
        // Check if link already exists
        var existingLink = await db.Table<Link>()
            .Where(l => l.SourceNoteId == sourceNoteId && l.TargetNoteId == targetNoteId)
            .FirstOrDefaultAsync();
        
        if (existingLink != null)
            return existingLink;
        
        var link = new Link
        {
            SourceNoteId = sourceNoteId,
            TargetNoteId = targetNoteId,
            LinkType = linkType,
            Context = context
        };
        
        await db.InsertAsync(link);
        return link;
    }

    /// <summary>
    /// Delete a link
    /// </summary>
    public async Task<bool> DeleteLinkAsync(int linkId)
    {
        var db = _databaseService.GetConnection();
        var result = await db.DeleteAsync<Link>(linkId);
        return result > 0;
    }

    /// <summary>
    /// Get graph data for visualization (nodes and edges)
    /// </summary>
    public async Task<GraphData> GetGraphDataAsync()
    {
        var notes = await _noteService.GetAllNotesAsync(limit: 500);
        var links = await GetAllLinksAsync();
        
        return new GraphData
        {
            Nodes = notes.Select(n => new GraphNode
            {
                Id = n.ID,
                Title = n.Title,
                IsPinned = n.IsPinned
            }).ToList(),
            Edges = links.Select(l => new GraphEdge
            {
                SourceId = l.SourceNoteId,
                TargetId = l.TargetNoteId,
                LinkType = l.LinkType
            }).ToList()
        };
    }
}

/// <summary>
/// Graph data structure for visualization
/// </summary>
public class GraphData
{
    public List<GraphNode> Nodes { get; set; } = new();
    public List<GraphEdge> Edges { get; set; } = new();
}

public class GraphNode
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsPinned { get; set; }
}

public class GraphEdge
{
    public int SourceId { get; set; }
    public int TargetId { get; set; }
    public string? LinkType { get; set; }
}
