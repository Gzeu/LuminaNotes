using LuminaNotes.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuminaNotes.Core.Services;

/// <summary>
/// Handles graph-related operations for note linking visualization
/// </summary>
public class GraphService
{
    private readonly DatabaseService _dbService;

    public GraphService(DatabaseService dbService)
    {
        _dbService = dbService;
    }

    /// <summary>
    /// Creates a link between two notes
    /// </summary>
    public async Task CreateLinkAsync(int sourceNoteId, int targetNoteId, string? context = null)
    {
        var conn = _dbService.GetConnection();
        
        // Check if link already exists
        var existing = await conn.Table<Link>()
            .Where(l => l.SourceNoteId == sourceNoteId && l.TargetNoteId == targetNoteId)
            .FirstOrDefaultAsync();

        if (existing != null) return;

        var link = new Link
        {
            SourceNoteId = sourceNoteId,
            TargetNoteId = targetNoteId,
            Context = context
        };

        await conn.InsertAsync(link);
    }

    /// <summary>
    /// Gets all links for a specific note
    /// </summary>
    public async Task<List<Link>> GetNoteLinksAsync(int noteId)
    {
        var conn = _dbService.GetConnection();
        
        var outgoing = await conn.Table<Link>()
            .Where(l => l.SourceNoteId == noteId)
            .ToListAsync();
        
        var incoming = await conn.Table<Link>()
            .Where(l => l.TargetNoteId == noteId)
            .ToListAsync();

        return outgoing.Concat(incoming).ToList();
    }

    /// <summary>
    /// Gets all links in the database for graph visualization
    /// </summary>
    public async Task<List<Link>> GetAllLinksAsync()
    {
        var conn = _dbService.GetConnection();
        return await conn.Table<Link>().ToListAsync();
    }

    /// <summary>
    /// Deletes a specific link
    /// </summary>
    public async Task DeleteLinkAsync(int linkId)
    {
        var conn = _dbService.GetConnection();
        await conn.DeleteAsync<Link>(linkId);
    }
}
