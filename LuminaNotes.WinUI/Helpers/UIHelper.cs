using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Threading.Tasks;

namespace LuminaNotes.WinUI.Helpers;

/// <summary>
/// Helper class for common UI operations and animations
/// </summary>
public static class UIHelper
{
    /// <summary>
    /// Animate element fade in
    /// </summary>
    public static void FadeIn(UIElement element, double durationMs = 300)
    {
        var animation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromMilliseconds(durationMs),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var storyboard = new Storyboard();
        Storyboard.SetTarget(animation, element);
        Storyboard.SetTargetProperty(animation, "Opacity");
        storyboard.Children.Add(animation);
        storyboard.Begin();
    }

    /// <summary>
    /// Animate element fade out
    /// </summary>
    public static async Task FadeOutAsync(UIElement element, double durationMs = 300)
    {
        var animation = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromMilliseconds(durationMs),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
        };

        var storyboard = new Storyboard();
        Storyboard.SetTarget(animation, element);
        Storyboard.SetTargetProperty(animation, "Opacity");
        storyboard.Children.Add(animation);
        
        var tcs = new TaskCompletionSource<bool>();
        storyboard.Completed += (s, e) => tcs.SetResult(true);
        storyboard.Begin();
        await tcs.Task;
    }

    /// <summary>
    /// Slide element in from bottom
    /// </summary>
    public static void SlideInFromBottom(UIElement element, double distance = 50, double durationMs = 400)
    {
        var translateAnimation = new DoubleAnimation
        {
            From = distance,
            To = 0,
            Duration = TimeSpan.FromMilliseconds(durationMs),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var opacityAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromMilliseconds(durationMs)
        };

        var storyboard = new Storyboard();
        Storyboard.SetTarget(translateAnimation, element);
        Storyboard.SetTargetProperty(translateAnimation, "(UIElement.RenderTransform).(TranslateTransform.Y)");
        Storyboard.SetTarget(opacityAnimation, element);
        Storyboard.SetTargetProperty(opacityAnimation, "Opacity");
        
        storyboard.Children.Add(translateAnimation);
        storyboard.Children.Add(opacityAnimation);
        storyboard.Begin();
    }

    /// <summary>
    /// Show success animation on element
    /// </summary>
    public static async Task ShowSuccessAsync(UIElement element)
    {
        var scaleAnimation = new DoubleAnimation
        {
            From = 1,
            To = 1.1,
            Duration = TimeSpan.FromMilliseconds(150),
            AutoReverse = true,
            EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.3 }
        };

        var storyboard = new Storyboard();
        Storyboard.SetTarget(scaleAnimation, element);
        Storyboard.SetTargetProperty(scaleAnimation, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
        storyboard.Children.Add(scaleAnimation);

        var scaleYAnimation = new DoubleAnimation
        {
            From = 1,
            To = 1.1,
            Duration = TimeSpan.FromMilliseconds(150),
            AutoReverse = true,
            EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.3 }
        };
        Storyboard.SetTarget(scaleYAnimation, element);
        Storyboard.SetTargetProperty(scaleYAnimation, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");
        storyboard.Children.Add(scaleYAnimation);

        var tcs = new TaskCompletionSource<bool>();
        storyboard.Completed += (s, e) => tcs.SetResult(true);
        storyboard.Begin();
        await tcs.Task;
    }

    /// <summary>
    /// Format file size for display
    /// </summary>
    public static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    /// <summary>
    /// Get relative time string
    /// </summary>
    public static string GetRelativeTime(DateTime dateTime)
    {
        var now = DateTime.Now;
        var diff = now - dateTime;

        if (diff.TotalSeconds < 60)
            return "just now";
        if (diff.TotalMinutes < 60)
            return $"{(int)diff.TotalMinutes}m ago";
        if (diff.TotalHours < 24)
            return $"{(int)diff.TotalHours}h ago";
        if (diff.TotalDays < 7)
            return $"{(int)diff.TotalDays}d ago";
        if (diff.TotalDays < 30)
            return $"{(int)(diff.TotalDays / 7)}w ago";
        if (dateTime.Year == now.Year)
            return dateTime.ToString("MMM dd");
        
        return dateTime.ToString("MMM dd, yyyy");
    }
}
