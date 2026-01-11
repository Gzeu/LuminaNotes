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

    [ObservableProperty]
    private Note? selectedNote;

    [ObservableProperty]
    private bool isLoading;

    public AllNotesViewModel(NoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task LoadNotesAsync()
    {
        IsLoading = true;
        try
        {
            var allNotes = await _noteService.GetAllNotesAsync(limit: 100);
            Notes.Clear();
            foreach (var note in allNotes)
            {
                Notes.Add(note);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task DeleteNoteAsync(Note note)
    {
        await _noteService.DeleteNoteAsync(note.Id);
        Notes.Remove(note);
    }
}
