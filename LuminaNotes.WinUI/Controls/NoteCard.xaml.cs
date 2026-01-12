using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;

namespace LuminaNotes.WinUI.Controls;

public sealed partial class NoteCard : UserControl
{
    public NoteCard()
    {
        this.InitializeComponent();
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(NoteCard),
            new PropertyMetadata(string.Empty, (d, e) => ((NoteCard)d).TitleText.Text = e.NewValue?.ToString() ?? ""));

    public string ContentPreviewText
    {
        get => (string)GetValue(ContentPreviewTextProperty);
        set => SetValue(ContentPreviewTextProperty, value);
    }

    public static readonly DependencyProperty ContentPreviewTextProperty =
        DependencyProperty.Register(nameof(ContentPreviewText), typeof(string), typeof(NoteCard),
            new PropertyMetadata(string.Empty, (d, e) => ((NoteCard)d).ContentPreview.Text = e.NewValue?.ToString() ?? ""));

    public bool IsPinned
    {
        get => (bool)GetValue(IsPinnedProperty);
        set => SetValue(IsPinnedProperty, value);
    }

    public static readonly DependencyProperty IsPinnedProperty =
        DependencyProperty.Register(nameof(IsPinned), typeof(bool), typeof(NoteCard),
            new PropertyMetadata(false, (d, e) => ((NoteCard)d).PinnedIcon.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed));

    public bool IsEncrypted
    {
        get => (bool)GetValue(IsEncryptedProperty);
        set => SetValue(IsEncryptedProperty, value);
    }

    public static readonly DependencyProperty IsEncryptedProperty =
        DependencyProperty.Register(nameof(IsEncrypted), typeof(bool), typeof(NoteCard),
            new PropertyMetadata(false, (d, e) => ((NoteCard)d).EncryptedIcon.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed));

    public string DateString
    {
        get => (string)GetValue(DateStringProperty);
        set => SetValue(DateStringProperty, value);
    }

    public static readonly DependencyProperty DateStringProperty =
        DependencyProperty.Register(nameof(DateString), typeof(string), typeof(NoteCard),
            new PropertyMetadata(string.Empty, (d, e) => ((NoteCard)d).DateText.Text = e.NewValue?.ToString() ?? ""));

    public List<string> Tags
    {
        get => (List<string>)GetValue(TagsProperty);
        set => SetValue(TagsProperty, value);
    }

    public static readonly DependencyProperty TagsProperty =
        DependencyProperty.Register(nameof(Tags), typeof(List<string>), typeof(NoteCard),
            new PropertyMetadata(new List<string>(), (d, e) => ((NoteCard)d).TagsList.ItemsSource = e.NewValue));

    private void CardRoot_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        var animation = new DoubleAnimation
        {
            To = -2,
            Duration = TimeSpan.FromMilliseconds(200),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var storyboard = new Storyboard();
        Storyboard.SetTarget(animation, CardTransform);
        Storyboard.SetTargetProperty(animation, "Y");
        storyboard.Children.Add(animation);
        storyboard.Begin();

        CardRoot.BorderBrush = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["AccentPrimaryBrush"];
    }

    private void CardRoot_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        var animation = new DoubleAnimation
        {
            To = 0,
            Duration = TimeSpan.FromMilliseconds(200),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var storyboard = new Storyboard();
        Storyboard.SetTarget(animation, CardTransform);
        Storyboard.SetTargetProperty(animation, "Y");
        storyboard.Children.Add(animation);
        storyboard.Begin();

        CardRoot.BorderBrush = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardStrokeColorDefaultBrush"];
    }
}
