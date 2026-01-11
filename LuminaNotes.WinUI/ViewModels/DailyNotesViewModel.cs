using CommunityToolkit.Mvvm.ComponentModel;
using LuminaNotes.Core.Models;
using LuminaNotes.Core.Services;
using LuminaNotes.Core.Utilities;
using System;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.ViewModels;

public partial class DailyNotesViewModel : ObservableObject
{
    private readonly NoteService _noteService;
    private Note? _currentNote;

    [ObservableProperty]
    private string todayTitle = $"Today: {DateTime.Now:dddd, MMMM dd, yyyy}";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WordCount))]
    private string noteContent = string.Empty;

    public string WordCount => $"{MarkdownHelper.GetWordCount(NoteContent)} words";

    public DailyNotesViewModel(NoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task LoadDailyNoteAsync()
    {
        var today = DateTime.Today;
        var dateString = today.ToString("yyyy-MM-dd");
        
        _currentNote = await _noteService.GetDailyNoteAsync(today);
        
        if (_currentNote == null)
        {
            _currentNote = new Note
            {
                Title = TodayTitle,
                Content = "",
                IsDailyNote = true,
                DailyNoteDate = dateString,
                Created = DateTime.Now
            };
            await _noteService.CreateNoteAsync(_currentNote);
        }

        NoteContent = _currentNote.Content;
    }

    partial void OnNoteContentChanged(string value)
    {
        // Auto-save logic (debounced)
        if (_currentNote != null)
        {
            _currentNote.Content = value;
            _ = Task.Run(async () => await _noteService.UpdateNoteAsync(_currentNote));
        }
    }
}
