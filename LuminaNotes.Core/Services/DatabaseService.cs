using SQLite;
using LuminaNotes.Core.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LuminaNotes.Core.Services;

/// <summary>
/// Manages SQLite database initialization and connection
/// </summary>
public class DatabaseService
{
    private readonly string _dbPath;
    private SQLiteAsyncConnection? _connection;

    public DatabaseService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var luminaFolder = Path.Combine(appDataPath, "LuminaNotes");
        Directory.CreateDirectory(luminaFolder);
        _dbPath = Path.Combine(luminaFolder, "LuminaNotes.db");
    }

    /// <summary>
    /// Initializes database connection and creates tables
    /// </summary>
    public async Task InitializeAsync()
    {
        if (_connection != null) return;

        _connection = new SQLiteAsyncConnection(_dbPath);
        
        // Create tables with indexes for performance
        await _connection.CreateTableAsync<Note>();
        await _connection.CreateTableAsync<Tag>();
        await _connection.CreateTableAsync<Link>();

        // Create indexes for common queries
        await _connection.ExecuteAsync(
            "CREATE INDEX IF NOT EXISTS idx_note_dailydate ON Note(DailyNoteDate)");
        await _connection.ExecuteAsync(
            "CREATE INDEX IF NOT EXISTS idx_note_created ON Note(Created DESC)");
        await _connection.ExecuteAsync(
            "CREATE INDEX IF NOT EXISTS idx_link_source ON Link(SourceNoteId)");
        await _connection.ExecuteAsync(
            "CREATE INDEX IF NOT EXISTS idx_link_target ON Link(TargetNoteId)");
    }

    /// <summary>
    /// Gets the active database connection
    /// </summary>
    public SQLiteAsyncConnection GetConnection()
    {
        if (_connection == null)
            throw new InvalidOperationException("Database not initialized. Call InitializeAsync first.");
        return _connection;
    }

    /// <summary>
    /// Gets the database file path
    /// </summary>
    public string GetDatabasePath() => _dbPath;
}
