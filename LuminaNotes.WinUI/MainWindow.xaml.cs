using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using LuminaNotes.WinUI.ViewModels;
using LuminaNotes.WinUI.Pages;
using System;
using LuminaNotes.Core.Services;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }
    private readonly AiService _aiService;

    public MainWindow()
    {
        this.InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<MainViewModel>();
        _aiService = Ioc.Default.GetRequiredService<AiService>();

        // Navigate to Daily Notes by default
        NavView.SelectedItem = NavView.MenuItems[0];
        ContentFrame.Navigate(typeof(DailyNotesPage));

        CheckAiAvailability();
    }

    private async void CheckAiAvailability()
    {
        var isAvailable = await _aiService.IsAiAvailableAsync();
        if (!isAvailable)
        {
            ShowStatus("Ollama not detected. AI features will be limited.", InfoBarSeverity.Warning);
        }
    }

    private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            ContentFrame.Navigate(typeof(SettingsPage));
            return;
        }

        var item = args.InvokedItemContainer as NavigationViewItem;
        var pageType = item?.Tag switch
        {
            "DailyNotes" => typeof(DailyNotesPage),
            "AllNotes" => typeof(AllNotesPage),
            "GraphView" => typeof(GraphViewPage),
            "Search" => typeof(SearchPage),
            _ => null
        };

        if (pageType != null)
            ContentFrame.Navigate(pageType);
    }

    private void NewNote_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement new note creation
        ShowStatus("New note feature coming soon!", InfoBarSeverity.Informational);
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement save
        ShowStatus("Note saved!", InfoBarSeverity.Success);
    }

    private void Search_Click(object sender, RoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(SearchPage));
    }

    private async void Summarize_Click(object sender, RoutedEventArgs e)
    {
        await ExecuteAiOperation(async () => 
            await _aiService.SummarizeAsync("Sample note content for testing summarization."));
    }

    private async void Rewrite_Click(object sender, RoutedEventArgs e)
    {
        await ExecuteAiOperation(async () => 
            await _aiService.RewriteAsync("Sample text", "professional"));
    }

    private async void Generate_Click(object sender, RoutedEventArgs e)
    {
        await ExecuteAiOperation(async () => 
            await _aiService.GenerateIdeasAsync("productivity"));
    }

    private async void Query_Click(object sender, RoutedEventArgs e)
    {
        var query = AiQueryBox.Text;
        if (string.IsNullOrWhiteSpace(query))
        {
            ShowStatus("Please enter a question", InfoBarSeverity.Warning);
            return;
        }

        await ExecuteAiOperation(async () => 
            await _aiService.QueryNotesAsync(query, "Your notes context here"));
    }

    private async void AiQueryBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter)
        {
            Query_Click(sender, e);
        }
    }

    private async Task ExecuteAiOperation(Func<Task<string>> operation)
    {
        try
        {
            AiResponseText.Text = "Processing...";
            var result = await operation();
            AiResponseText.Text = result;
            ShowStatus("AI operation completed", InfoBarSeverity.Success);
        }
        catch (Exception ex)
        {
            AiResponseText.Text = $"Error: {ex.Message}";
            ShowStatus("AI operation failed", InfoBarSeverity.Error);
        }
    }

    public void ShowStatus(string message, InfoBarSeverity severity = InfoBarSeverity.Informational)
    {
        StatusBar.Message = message;
        StatusBar.Severity = severity;
        StatusBar.IsOpen = true;
    }
}
