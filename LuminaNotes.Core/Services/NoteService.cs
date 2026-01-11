using LuminaNotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LuminaNotes.Core.Services;

/// <summary>
/// Handles CRUD operations for notes
/// </summary>
public class NoteService
{
    private readonly DatabaseService _databaseService;

    public NoteService(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    /// <summary>
    /// Create a new note
    /// </summary>
    public async Task<Note> CreateNoteAsync(Note note)
    {
        var db = _databaseService.GetConnection();
        note.Created = DateTime.Now;
        note.Updated = DateTime.Now;
        await db.InsertAsync(note);
        return note;
    }

    /// <summary>
    /// Update existing note
    /// </summary>
    public async Task<bool> UpdateNoteAsync(Note note)
    {
        var db = _databaseService.GetConnection();
        note.Updated = DateTime.Now;
        var result = await db.UpdateAsync(note);
        return result > 0;
    }

    /// <summary>
    /// Delete note by ID
    /// </summary>
    public async Task<bool> DeleteNoteAsync(int noteId)
    {
        var db = _databaseService.GetConnection();
        var result = await db.DeleteAsync<Note>(noteId);
        return result > 0;
    }

    /// <summary>
    /// Get note by ID
    /// </summary>
    public async Task<Note?> GetNoteByIdAsync(int noteId)
    {
        var db = _databaseService.GetConnection();
        return await db.FindAsync<Note>(noteId);
    }

    /// <summary>
    /// Get daily note for specific date (or create if doesn't exist)
    /// </summary>
    public async Task<Note?> GetDailyNoteAsync(DateTime date)
    {
        var db = _databaseService.GetConnection();
        var dateString = date.ToString("yyyy-MM-dd");
        var note = await db.Table<Note>()
            .Where(n => n.DailyNoteDate == dateString)
            .FirstOrDefaultAsync();
        
        return note;
    }

    /// <summary>
    /// Get all notes (with optional limit)
    /// </summary>
    public async Task<List<Note>> GetAllNotesAsync(int limit = 100, int offset = 0)
    {
        var db = _databaseService.GetConnection();
        return await db.Table<Note>()
            .OrderByDescending(n => n.Updated)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    /// <summary>
    /// Search notes by title or content
    /// </summary>
    public async Task<List<Note>> SearchNotesAsync(string query)
    {
        var db = _databaseService.GetConnection();
        var lowerQuery = query.ToLower();
        return await db.Table<Note>()
            .Where(n => n.Title.ToLower().Contains(lowerQuery) || n.Content.ToLower().Contains(lowerQuery))
            .OrderByDescending(n => n.Updated)
            .ToListAsync();
    }

    /// <summary>
    /// Get notes by tag
    /// </summary>
    public async Task<List<Note>> GetNotesByTagAsync(string tagName)
    {
        var db = _databaseService.GetConnection();
        var allNotes = await db.Table<Note>().ToListAsync();
        
        return allNotes.Where(n => 
        {
            try
            {
                var tags = JsonSerializer.Deserialize<List<string>>(n.Tags);
                return tags?.Contains(tagName, StringComparer.OrdinalIgnoreCase) ?? false;
            }
            catch
            {
                return false;
            }
        }).ToList();
    }

    /// <summary>
    /// Get pinned notes
    /// </summary>
    public async Task<List<Note>> GetPinnedNotesAsync()
    {
        var db = _databaseService.GetConnection();
        return await db.Table<Note>()
            .Where(n => n.IsPinned)
            .OrderByDescending(n => n.Updated)
            .ToListAsync();
    }
}
