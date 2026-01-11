using LuminaNotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LuminaNotes.Core.Services;

/// <summary>
/// Handles CRUD operations and business logic for notes
/// </summary>
public class NoteService
{
    private readonly DatabaseService _dbService;

    public NoteService(DatabaseService dbService)
    {
        _dbService = dbService;
    }

    /// <summary>
    /// Creates a new note in the database
    /// </summary>
    public async Task<Note> CreateNoteAsync(Note note)
    {
        note.Created = DateTime.Now;
        note.Updated = DateTime.Now;
        
        var conn = _dbService.GetConnection();
        await conn.InsertAsync(note);
        return note;
    }

    /// <summary>
    /// Updates an existing note
    /// </summary>
    public async Task UpdateNoteAsync(Note note)
    {
        note.Updated = DateTime.Now;
        var conn = _dbService.GetConnection();
        await conn.UpdateAsync(note);
    }

    /// <summary>
    /// Deletes a note by ID
    /// </summary>
    public async Task DeleteNoteAsync(int noteId)
    {
        var conn = _dbService.GetConnection();
        await conn.DeleteAsync<Note>(noteId);
        
        // Clean up orphaned links
        await conn.ExecuteAsync(
            "DELETE FROM Link WHERE SourceNoteId = ? OR TargetNoteId = ?", noteId, noteId);
    }

    /// <summary>
    /// Gets a note by ID
    /// </summary>
    public async Task<Note?> GetNoteByIdAsync(int noteId)
    {
        var conn = _dbService.GetConnection();
        return await conn.FindAsync<Note>(noteId);
    }

    /// <summary>
    /// Gets the daily note for a specific date, creating it if it doesn't exist
    /// </summary>
    public async Task<Note> GetDailyNoteAsync(DateTime date)
    {
        var conn = _dbService.GetConnection();
        var dateOnly = date.Date;
        
        var existing = await conn.Table<Note>()
            .Where(n => n.IsDailyNote && n.DailyNoteDate == dateOnly)
            .FirstOrDefaultAsync();

        if (existing != null)
            return existing;

        // Create new daily note
        var newNote = new Note
        {
            Title = $"Daily Note - {date:MMMM dd, yyyy}",
            Content = string.Empty,
            IsDailyNote = true,
            DailyNoteDate = dateOnly,
            Created = DateTime.Now,
            Updated = DateTime.Now
        };

        await CreateNoteAsync(newNote);
        return newNote;
    }

    /// <summary>
    /// Gets all notes ordered by last updated
    /// </summary>
    public async Task<List<Note>> GetAllNotesAsync(int limit = 100, int offset = 0)
    {
        var conn = _dbService.GetConnection();
        return await conn.Table<Note>()
            .OrderByDescending(n => n.Updated)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    /// <summary>
    /// Searches notes by title or content
    /// </summary>
    public async Task<List<Note>> SearchNotesAsync(string query)
    {
        var conn = _dbService.GetConnection();
        var searchTerm = $"%{query}%";
        
        return await conn.QueryAsync<Note>(
            "SELECT * FROM Note WHERE Title LIKE ? OR Content LIKE ? ORDER BY Updated DESC LIMIT 50",
            searchTerm, searchTerm);
    }

    /// <summary>
    /// Gets notes by tag ID
    /// </summary>
    public async Task<List<Note>> GetNotesByTagAsync(int tagId)
    {
        var conn = _dbService.GetConnection();
        var allNotes = await conn.Table<Note>().ToListAsync();
        
        return allNotes.Where(note =>
        {
            var tagIds = JsonSerializer.Deserialize<List<int>>(note.Tags) ?? new List<int>();
            return tagIds.Contains(tagId);
        }).ToList();
    }
}
