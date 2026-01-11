using CommunityToolkit.Mvvm.ComponentModel;
using LuminaNotes.Core.Models;
using LuminaNotes.Core.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    private readonly NoteService _noteService;

    [ObservableProperty]
    private string searchQuery = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Note> searchResults = new();

    public SearchViewModel(NoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            SearchResults.Clear();
            return;
        }

        var results = await _noteService.SearchNotesAsync(SearchQuery);
        SearchResults.Clear();
        foreach (var note in results)
        {
            SearchResults.Add(note);
        }
    }
}
