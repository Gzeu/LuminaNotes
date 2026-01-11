using CommunityToolkit.Mvvm.ComponentModel;
using LuminaNotes.Core.Models;
using LuminaNotes.Core.Services;
using System;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.ViewModels;

public partial class DailyNotesViewModel : ObservableObject
{
    private readonly NoteService _noteService;

    [ObservableProperty]
    private string todayTitle = $"Today: {DateTime.Now:MMMM dd, yyyy}";

    [ObservableProperty]
    private string noteContent = string.Empty;

    [ObservableProperty]
    private Note? currentNote;

    public DailyNotesViewModel(NoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task LoadDailyNoteAsync()
    {
        CurrentNote = await _noteService.GetDailyNoteAsync(DateTime.Today);
        NoteContent = CurrentNote?.Content ?? string.Empty;
        TodayTitle = $"Today: {DateTime.Now:dddd, MMMM dd, yyyy}";
    }

    public async Task SaveNoteAsync()
    {
        if (CurrentNote == null) return;

        CurrentNote.Content = NoteContent;
        await _noteService.UpdateNoteAsync(CurrentNote);
    }
}
