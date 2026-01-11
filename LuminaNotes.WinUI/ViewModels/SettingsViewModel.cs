using CommunityToolkit.Mvvm.ComponentModel;
using LuminaNotes.Core.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;
    private readonly AiService _aiService;

    [ObservableProperty]
    private string ollamaEndpoint = "http://localhost:11434";

    [ObservableProperty]
    private string selectedModel = "llama3.1:8b";

    [ObservableProperty]
    private ObservableCollection<string> availableModels = new();

    [ObservableProperty]
    private bool encryptionEnabled = false;

    [ObservableProperty]
    private string databasePath = string.Empty;

    [ObservableProperty]
    private string connectionStatus = "Not tested";

    public SettingsViewModel(DatabaseService databaseService, AiService aiService)
    {
        _databaseService = databaseService;
        _aiService = aiService;
    }

    public async Task LoadSettingsAsync()
    {
        DatabasePath = _databaseService.GetDatabasePath();
        
        var models = await _aiService.GetAvailableModelsAsync();
        AvailableModels.Clear();
        foreach (var model in models)
        {
            AvailableModels.Add(model);
        }

        if (AvailableModels.Count == 0)
        {
            AvailableModels.Add("llama3.1:8b");
        }
    }

    public async Task TestAiConnectionAsync()
    {
        var isAvailable = await _aiService.IsAiAvailableAsync();
        ConnectionStatus = isAvailable ? "✓ Connected" : "✗ Connection failed";
    }
}
