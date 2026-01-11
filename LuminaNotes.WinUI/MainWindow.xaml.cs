using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using LuminaNotes.WinUI.ViewModels;
using LuminaNotes.WinUI.Pages;
using System;

namespace LuminaNotes.WinUI;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        InitializeComponent();
        ViewModel = App.Services.GetRequiredService<MainViewModel>();
        
        // Navigate to Daily Notes initially
        NavView.SelectedItem = NavView.MenuItems[0];
        ContentFrame.Navigate(typeof(DailyNotesPage));
    }

    private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            ContentFrame.Navigate(typeof(SettingsPage));
            return;
        }

        if (args.InvokedItemContainer is NavigationViewItem item)
        {
            var pageType = item.Tag switch
            {
                "DailyNotes" => typeof(DailyNotesPage),
                "AllNotes" => typeof(AllNotesPage),
                "GraphView" => typeof(GraphViewPage),
                "Search" => typeof(SearchPage),
                _ => null
            };

            if (pageType != null)
            {
                ContentFrame.Navigate(pageType);
            }
        }
    }

    public void ShowStatus(string message, InfoBarSeverity severity = InfoBarSeverity.Informational)
    {
        DispatcherQueue.TryEnqueue(() =>
        {
            StatusBar.Message = message;
            StatusBar.Severity = severity;
            StatusBar.IsOpen = true;
        });
    }

    private async void NewNote_Click(object sender, RoutedEventArgs e)
    {
        await ViewModel.CreateNewNoteAsync();
        ShowStatus("New note created", InfoBarSeverity.Success);
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement save logic
        ShowStatus("Note saved", InfoBarSeverity.Success);
    }

    private async void Summarize_Click(object sender, RoutedEventArgs e)
    {
        ShowStatus("Generating summary...", InfoBarSeverity.Informational);
        var result = await ViewModel.SummarizeCurrentNoteAsync();
        ShowStatus(result, InfoBarSeverity.Success);
    }

    private async void Rewrite_Click(object sender, RoutedEventArgs e)
    {
        ShowStatus("Rewriting text...", InfoBarSeverity.Informational);
        var result = await ViewModel.RewriteCurrentNoteAsync();
        ShowStatus(result, InfoBarSeverity.Success);
    }

    private async void Generate_Click(object sender, RoutedEventArgs e)
    {
        ShowStatus("Generating ideas...", InfoBarSeverity.Informational);
        var result = await ViewModel.GenerateIdeasAsync();
        ShowStatus(result, InfoBarSeverity.Success);
    }

    private void Query_Click(object sender, RoutedEventArgs e)
    {
        ShowStatus("Q&A feature coming soon", InfoBarSeverity.Warning);
    }
}
