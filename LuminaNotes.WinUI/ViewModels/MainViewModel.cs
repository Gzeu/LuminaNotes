using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuminaNotes.Core.Services;
using LuminaNotes.Core.Models;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly NoteService _noteService;
    private readonly AiService _aiService;

    [ObservableProperty]
    private bool isAiSidebarOpen = true;

    [ObservableProperty]
    private string statusMessage = "Ready";

    public MainViewModel(NoteService noteService, AiService aiService)
    {
        _noteService = noteService;
        _aiService = aiService;
    }

    [RelayCommand]
    private async Task NewNoteAsync()
    {
        var newNote = new Note
        {
            Title = "New Note",
            Content = "",
            Created = System.DateTime.Now
        };

        await _noteService.CreateNoteAsync(newNote);
        StatusMessage = "New note created";
    }

    [RelayCommand]
    private void ToggleAiSidebar()
    {
        IsAiSidebarOpen = !IsAiSidebarOpen;
    }
}
