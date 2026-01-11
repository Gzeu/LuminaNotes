using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuminaNotes.Core.Services;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly AiService _aiService;
    private readonly DatabaseService _dbService;

    [ObservableProperty]
    private string ollamaModel = "llama3.1:8b";

    [ObservableProperty]
    private bool isAiAvailable;

    [ObservableProperty]
    private ElementTheme selectedTheme = ElementTheme.Dark;

    [ObservableProperty]
    private string databasePath = string.Empty;

    public SettingsViewModel(AiService aiService, DatabaseService dbService)
    {
        _aiService = aiService;
        _dbService = dbService;
        DatabasePath = _dbService.GetDatabasePath();
    }

    [RelayCommand]
    private async Task CheckAiStatusAsync()
    {
        IsAiAvailable = await _aiService.IsAiAvailableAsync();
    }

    [RelayCommand]
    private void ChangeModel()
    {
        _aiService.SetModel(OllamaModel);
    }

    [RelayCommand]
    private void ChangeTheme()
    {
        // TODO: Implement theme changing logic
    }
}
