using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using System;

namespace LuminaNotes.WinUI.Controls;

public sealed partial class AnimatedCard : UserControl
{
    public AnimatedCard()
    {
        this.InitializeComponent();
    }

    public object CardContent
    {
        get => GetValue(CardContentProperty);
        set => SetValue(CardContentProperty, value);
    }

    public static readonly DependencyProperty CardContentProperty =
        DependencyProperty.Register(nameof(CardContent), typeof(object), typeof(AnimatedCard), 
            new PropertyMetadata(null, OnCardContentChanged));

    private static void OnCardContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AnimatedCard card)
        {
            card.ContentPresenter.Content = e.NewValue;
        }
    }

    private void CardBorder_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        var scaleAnimation = new DoubleAnimation
        {
            To = 1.02,
            Duration = TimeSpan.FromMilliseconds(150),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var storyboard = new Storyboard();
        Storyboard.SetTarget(scaleAnimation, CardTransform);
        Storyboard.SetTargetProperty(scaleAnimation, "ScaleX");
        storyboard.Children.Add(scaleAnimation);

        var scaleYAnimation = new DoubleAnimation
        {
            To = 1.02,
            Duration = TimeSpan.FromMilliseconds(150),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };
        Storyboard.SetTarget(scaleYAnimation, CardTransform);
        Storyboard.SetTargetProperty(scaleYAnimation, "ScaleY");
        storyboard.Children.Add(scaleYAnimation);

        var elevationAnimation = new DoubleAnimation
        {
            To = 8,
            Duration = TimeSpan.FromMilliseconds(150)
        };
        Storyboard.SetTarget(elevationAnimation, CardTransform);
        Storyboard.SetTargetProperty(elevationAnimation, "TranslateZ");
        storyboard.Children.Add(elevationAnimation);

        storyboard.Begin();
    }

    private void CardBorder_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        var scaleAnimation = new DoubleAnimation
        {
            To = 1,
            Duration = TimeSpan.FromMilliseconds(150),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var storyboard = new Storyboard();
        Storyboard.SetTarget(scaleAnimation, CardTransform);
        Storyboard.SetTargetProperty(scaleAnimation, "ScaleX");
        storyboard.Children.Add(scaleAnimation);

        var scaleYAnimation = new DoubleAnimation
        {
            To = 1,
            Duration = TimeSpan.FromMilliseconds(150),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };
        Storyboard.SetTarget(scaleYAnimation, CardTransform);
        Storyboard.SetTargetProperty(scaleYAnimation, "ScaleY");
        storyboard.Children.Add(scaleYAnimation);

        var elevationAnimation = new DoubleAnimation
        {
            To = 0,
            Duration = TimeSpan.FromMilliseconds(150)
        };
        Storyboard.SetTarget(elevationAnimation, CardTransform);
        Storyboard.SetTargetProperty(elevationAnimation, "TranslateZ");
        storyboard.Children.Add(elevationAnimation);

        storyboard.Begin();
    }

    private void CardBorder_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        var scaleAnimation = new DoubleAnimation
        {
            To = 0.98,
            Duration = TimeSpan.FromMilliseconds(100)
        };

        var storyboard = new Storyboard();
        Storyboard.SetTarget(scaleAnimation, CardTransform);
        Storyboard.SetTargetProperty(scaleAnimation, "ScaleX");
        storyboard.Children.Add(scaleAnimation);

        var scaleYAnimation = new DoubleAnimation
        {
            To = 0.98,
            Duration = TimeSpan.FromMilliseconds(100)
        };
        Storyboard.SetTarget(scaleYAnimation, CardTransform);
        Storyboard.SetTargetProperty(scaleYAnimation, "ScaleY");
        storyboard.Children.Add(scaleYAnimation);

        storyboard.Begin();
    }

    private void CardBorder_PointerReleased(object sender, PointerRoutedEventArgs e)
    {
        CardBorder_PointerEntered(sender, e);
    }
}
