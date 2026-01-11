using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LuminaNotes.WinUI.ViewModels;

namespace LuminaNotes.WinUI.Pages;

public sealed partial class AllNotesPage : Page
{
    public AllNotesViewModel ViewModel { get; }

    public AllNotesPage()
    {
        this.InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<AllNotesViewModel>();
        Loaded += async (_, _) => await ViewModel.LoadNotesAsync();
    }
}
