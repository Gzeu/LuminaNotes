using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using LuminaNotes.Core.Models;
using LuminaNotes.Core.Services;
using System;
using System.Globalization;

namespace LuminaNotes.WinUI.Services;

/// <summary>
/// Applies design tokens to WinUI ResourceDictionary
/// </summary>
public class ThemeApplier
{
    private readonly ThemeService _themeService;
    private readonly ResourceDictionary _resources;

    public ThemeApplier(ThemeService themeService)
    {
        _themeService = themeService;
        _resources = Application.Current.Resources;
        
        // Subscribe to theme changes
        _themeService.ThemeChanged += OnThemeChanged;
    }

    /// <summary>
    /// Apply tokens to application resources
    /// </summary>
    public void ApplyTokens(DesignTokens tokens)
    {
        ApplyColorTokens(tokens.Colors);
        ApplyTypographyTokens(tokens.Typography);
        ApplySpacingTokens(tokens.Spacing);
        ApplyRadiusTokens(tokens.Radius);
        ApplyShadowTokens(tokens.Shadows);
        ApplyAnimationTokens(tokens.Animations);
    }

    private void OnThemeChanged(object? sender, DesignTokens tokens)
    {
        ApplyTokens(tokens);
    }

    private void ApplyColorTokens(ColorTokens colors)
    {
        // Accent Colors
        _resources["AccentPrimary"] = ParseColor(colors.AccentPrimary);
        _resources["AccentSecondary"] = ParseColor(colors.AccentSecondary);
        _resources["AccentTertiary"] = ParseColor(colors.AccentTertiary);

        _resources["AccentPrimaryBrush"] = new SolidColorBrush(ParseColor(colors.AccentPrimary));
        _resources["AccentSecondaryBrush"] = new SolidColorBrush(ParseColor(colors.AccentSecondary));
        _resources["AccentTertiaryBrush"] = new SolidColorBrush(ParseColor(colors.AccentTertiary));

        // Semantic Colors
        _resources["SuccessColor"] = ParseColor(colors.Success);
        _resources["WarningColor"] = ParseColor(colors.Warning);
        _resources["ErrorColor"] = ParseColor(colors.Error);
        _resources["InfoColor"] = ParseColor(colors.Info);

        // Update gradient
        var gradientBrush = new LinearGradientBrush
        {
            StartPoint = new Windows.Foundation.Point(0, 0),
            EndPoint = new Windows.Foundation.Point(1, 1)
        };
        gradientBrush.GradientStops.Add(new GradientStop { Color = ParseColor(colors.AccentPrimary), Offset = 0 });
        gradientBrush.GradientStops.Add(new GradientStop { Color = ParseColor(colors.AccentSecondary), Offset = 0.5 });
        gradientBrush.GradientStops.Add(new GradientStop { Color = ParseColor(colors.AccentTertiary), Offset = 1 });
        _resources["AccentGradientBrush"] = gradientBrush;
    }

    private void ApplyTypographyTokens(TypographyTokens typography)
    {
        _resources["FontSizeHero"] = typography.FontSizeHero;
        _resources["FontSizeTitle"] = typography.FontSizeTitle;
        _resources["FontSizeSubtitle"] = typography.FontSizeSubtitle;
        _resources["FontSizeBody"] = typography.FontSizeBody;
        _resources["FontSizeCaption"] = typography.FontSizeCaption;
        _resources["FontSizeCode"] = typography.FontSizeCode;

        _resources["FontFamilyPrimary"] = new FontFamily(typography.FontFamilyPrimary);
        _resources["FontFamilyCode"] = new FontFamily(typography.FontFamilyCode);
    }

    private void ApplySpacingTokens(SpacingTokens spacing)
    {
        _resources["SpacingXS"] = spacing.XS;
        _resources["SpacingS"] = spacing.S;
        _resources["SpacingM"] = spacing.M;
        _resources["SpacingL"] = spacing.L;
        _resources["SpacingXL"] = spacing.XL;
        _resources["SpacingXXL"] = spacing.XXL;
        _resources["SpacingXXXL"] = spacing.XXXL;
    }

    private void ApplyRadiusTokens(RadiusTokens radius)
    {
        _resources["ControlCornerRadius"] = new CornerRadius(radius.Medium);
        _resources["CardCornerRadius"] = new CornerRadius(radius.Large);
        _resources["SmallCornerRadius"] = new CornerRadius(radius.Small);
    }

    private void ApplyShadowTokens(ShadowTokens shadows)
    {
        _resources["ElevationShadowDepth1"] = shadows.ElevationSmall;
        _resources["ElevationShadowDepth2"] = shadows.ElevationMedium;
        _resources["ElevationShadowDepth3"] = shadows.ElevationLarge;
    }

    private void ApplyAnimationTokens(AnimationTokens animations)
    {
        _resources["FastDuration"] = TimeSpan.FromMilliseconds(animations.DurationFast);
        _resources["MediumDuration"] = TimeSpan.FromMilliseconds(animations.DurationMedium);
        _resources["SlowDuration"] = TimeSpan.FromMilliseconds(animations.DurationSlow);
    }

    private Windows.UI.Color ParseColor(string hex)
    {
        hex = hex.TrimStart('#');
        if (hex.Length == 6)
        {
            return Windows.UI.Color.FromArgb(
                255,
                byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber),
                byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber),
                byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber)
            );
        }
        return Windows.UI.Colors.Transparent;
    }
}
