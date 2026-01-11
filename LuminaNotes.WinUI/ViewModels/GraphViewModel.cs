using CommunityToolkit.Mvvm.ComponentModel;
using LuminaNotes.Core.Services;

namespace LuminaNotes.WinUI.ViewModels;

public partial class GraphViewModel : ObservableObject
{
    private readonly GraphService _graphService;

    public GraphViewModel(GraphService graphService)
    {
        _graphService = graphService;
    }

    // TODO: Implement graph data loading and visualization
}
