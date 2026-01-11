using CommunityToolkit.Mvvm.ComponentModel;
using LuminaNotes.Core.Models;
using LuminaNotes.Core.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.ViewModels;

public partial class GraphViewModel : ObservableObject
{
    private readonly GraphService _graphService;
    private readonly NoteService _noteService;

    [ObservableProperty]
    private ObservableCollection<Link> links = new();

    [ObservableProperty]
    private ObservableCollection<Note> nodes = new();

    [ObservableProperty]
    private bool isLoading;

    public GraphViewModel(GraphService graphService, NoteService noteService)
    {
        _graphService = graphService;
        _noteService = noteService;
    }

    public async Task LoadGraphDataAsync()
    {
        IsLoading = true;
        try
        {
            var allLinks = await _graphService.GetAllLinksAsync();
            var allNotes = await _noteService.GetAllNotesAsync(limit: 500);

            Links.Clear();
            Nodes.Clear();

            foreach (var link in allLinks)
            {
                Links.Add(link);
            }

            foreach (var note in allNotes)
            {
                Nodes.Add(note);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }
}
