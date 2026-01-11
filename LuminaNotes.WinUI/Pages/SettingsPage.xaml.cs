using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LuminaNotes.WinUI.ViewModels;

namespace LuminaNotes.WinUI.Pages;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel { get; }

    public SettingsPage()
    {
        this.InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<SettingsViewModel>();
        Loaded += async (_, _) => await ViewModel.LoadSettingsAsync();
    }

    private async void TestConnection_Click(object sender, RoutedEventArgs e)
    {
        await ViewModel.TestAiConnectionAsync();
    }
}
