using CommunityToolkit.Mvvm.ComponentModel;
using LuminaNotes.Core.Models;
using LuminaNotes.Core.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.ViewModels;

public partial class AllNotesViewModel : ObservableObject
{
    private readonly NoteService _noteService;

    [ObservableProperty]
    private ObservableCollection<Note> notes = new();

    public AllNotesViewModel(NoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task LoadNotesAsync()
    {
        var allNotes = await _noteService.GetAllNotesAsync(limit: 100);
        Notes.Clear();
        foreach (var note in allNotes)
        {
            Notes.Add(note);
        }
    }
}
