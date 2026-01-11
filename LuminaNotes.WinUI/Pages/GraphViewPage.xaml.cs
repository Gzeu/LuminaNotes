using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LuminaNotes.WinUI.ViewModels;

namespace LuminaNotes.WinUI.Pages;

public sealed partial class GraphViewPage : Page
{
    public GraphViewModel ViewModel { get; }

    public GraphViewPage()
    {
        this.InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<GraphViewModel>();
    }
}
