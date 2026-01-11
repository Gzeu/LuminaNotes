using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LuminaNotes.WinUI.ViewModels;

namespace LuminaNotes.WinUI.Pages;

public sealed partial class DailyNotesPage : Page
{
    public DailyNotesViewModel ViewModel { get; }

    public DailyNotesPage()
    {
        this.InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<DailyNotesViewModel>();
        Loaded += async (_, _) => await ViewModel.LoadDailyNoteAsync();
    }
}
