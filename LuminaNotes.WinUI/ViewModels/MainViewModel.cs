using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuminaNotes.Core.Models;
using LuminaNotes.Core.Services;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly NoteService _noteService;
    private readonly AiService _aiService;

    [ObservableProperty]
    private bool isAiSidebarOpen = true;

    [ObservableProperty]
    private double aiSidebarWidth = 340;

    [ObservableProperty]
    private Note? currentNote;

    public MainViewModel(NoteService noteService, AiService aiService)
    {
        _noteService = noteService;
        _aiService = aiService;
    }

    public async Task CreateNewNoteAsync()
    {
        var newNote = new Note
        {
            Title = "Untitled Note",
            Content = string.Empty
        };

        CurrentNote = await _noteService.CreateNoteAsync(newNote);
    }

    public async Task<string> SummarizeCurrentNoteAsync()
    {
        if (CurrentNote == null || string.IsNullOrEmpty(CurrentNote.Content))
            return "No note content to summarize.";

        var summary = await _aiService.SummarizeAsync(CurrentNote.Content);
        return $"Summary: {summary}";
    }

    public async Task<string> RewriteCurrentNoteAsync(string style = "professional")
    {
        if (CurrentNote == null || string.IsNullOrEmpty(CurrentNote.Content))
            return "No note content to rewrite.";

        return await _aiService.RewriteAsync(CurrentNote.Content, style);
    }

    public async Task<string> GenerateIdeasAsync()
    {
        var topic = CurrentNote?.Title ?? "general topics";
        return await _aiService.GenerateIdeasAsync(topic);
    }
}
