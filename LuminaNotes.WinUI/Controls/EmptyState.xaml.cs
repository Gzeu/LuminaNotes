using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace LuminaNotes.WinUI.Controls;

public sealed partial class EmptyState : UserControl
{
    public EmptyState()
    {
        this.InitializeComponent();
    }

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(string), typeof(EmptyState),
            new PropertyMetadata("\uE74C", (d, e) => ((EmptyState)d).IconGlyph.Glyph = e.NewValue?.ToString() ?? "\uE74C"));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(EmptyState),
            new PropertyMetadata("No items found", (d, e) => ((EmptyState)d).TitleText.Text = e.NewValue?.ToString() ?? ""));

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(EmptyState),
            new PropertyMetadata("Get started by creating your first item", (d, e) => ((EmptyState)d).DescriptionText.Text = e.NewValue?.ToString() ?? ""));

    public string ActionButtonText
    {
        get => (string)GetValue(ActionButtonTextProperty);
        set => SetValue(ActionButtonTextProperty, value);
    }

    public static readonly DependencyProperty ActionButtonTextProperty =
        DependencyProperty.Register(nameof(ActionButtonText), typeof(string), typeof(EmptyState),
            new PropertyMetadata(string.Empty, OnActionButtonTextChanged));

    private static void OnActionButtonTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EmptyState emptyState)
        {
            var text = e.NewValue?.ToString() ?? string.Empty;
            emptyState.ActionButton.Content = text;
            emptyState.ActionButton.Visibility = string.IsNullOrEmpty(text) ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public event EventHandler<RoutedEventArgs>? ActionButtonClicked;

    private void ActionButton_Click(object sender, RoutedEventArgs e)
    {
        ActionButtonClicked?.Invoke(this, e);
    }
}
