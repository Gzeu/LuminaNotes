using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace LuminaNotes.WinUI.Controls;

public sealed partial class LoadingIndicator : UserControl
{
    public LoadingIndicator()
    {
        this.InitializeComponent();
    }

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(string), typeof(LoadingIndicator),
            new PropertyMetadata("Loading...", (d, e) => ((LoadingIndicator)d).LoadingText.Text = e.NewValue?.ToString() ?? "Loading..."));
}
