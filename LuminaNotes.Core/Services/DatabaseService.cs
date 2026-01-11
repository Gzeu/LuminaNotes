using SQLite;
using LuminaNotes.Core.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LuminaNotes.Core.Services;

/// <summary>
/// Manages SQLite database initialization and connections
/// </summary>
public class DatabaseService
{
    private readonly string _dbPath;
    private SQLiteAsyncConnection? _connection;

    public DatabaseService()
    {
        _dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "LuminaNotes",
            "LuminaNotes.db"
        );
    }

    /// <summary>
    /// Initialize database and create tables if they don't exist
    /// </summary>
    public async Task InitializeAsync()
    {
        if (_connection != null) return;

        // Ensure directory exists
        var dbDir = Path.GetDirectoryName(_dbPath);
        if (!string.IsNullOrEmpty(dbDir) && !Directory.Exists(dbDir))
        {
            Directory.CreateDirectory(dbDir);
        }

        _connection = new SQLiteAsyncConnection(_dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);

        // Create tables
        await _connection.CreateTableAsync<Note>();
        await _connection.CreateTableAsync<Tag>();
        await _connection.CreateTableAsync<Link>();

        // Create indexes for performance
        await _connection.ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_notes_title ON Note(Title)");
        await _connection.ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_notes_dailydate ON Note(DailyNoteDate)");
        await _connection.ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_notes_created ON Note(Created)");
    }

    /// <summary>
    /// Get database connection (must call InitializeAsync first)
    /// </summary>
    public SQLiteAsyncConnection GetConnection()
    {
        if (_connection == null)
            throw new InvalidOperationException("Database not initialized. Call InitializeAsync first.");
        return _connection;
    }

    /// <summary>
    /// Close database connection
    /// </summary>
    public async Task CloseAsync()
    {
        if (_connection != null)
        {
            await _connection.CloseAsync();
            _connection = null;
        }
    }

    /// <summary>
    /// Get database file path
    /// </summary>
    public string GetDatabasePath() => _dbPath;
}
