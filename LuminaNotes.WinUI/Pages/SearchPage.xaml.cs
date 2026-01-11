using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LuminaNotes.WinUI.ViewModels;

namespace LuminaNotes.WinUI.Pages;

public sealed partial class SearchPage : Page
{
    public SearchViewModel ViewModel { get; }

    public SearchPage()
    {
        this.InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<SearchViewModel>();
    }

    private async void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        await ViewModel.SearchAsync();
    }
}
