using System;
using System.Text.Json.Serialization;

namespace LuminaNotes.Core.Models;

/// <summary>
/// Design tokens for consistent styling across the application
/// </summary>
public class DesignTokens
{
    public ColorTokens Colors { get; set; } = new();
    public TypographyTokens Typography { get; set; } = new();
    public SpacingTokens Spacing { get; set; } = new();
    public RadiusTokens Radius { get; set; } = new();
    public ShadowTokens Shadows { get; set; } = new();
    public AnimationTokens Animations { get; set; } = new();
}

public class ColorTokens
{
    // Primary Colors
    public string AccentPrimary { get; set; } = "#6366F1";
    public string AccentSecondary { get; set; } = "#8B5CF6";
    public string AccentTertiary { get; set; } = "#EC4899";

    // Semantic Colors
    public string Success { get; set; } = "#10B981";
    public string Warning { get; set; } = "#F59E0B";
    public string Error { get; set; } = "#EF4444";
    public string Info { get; set; } = "#3B82F6";

    // Neutral Colors (Light Theme)
    public string BackgroundPrimary { get; set; } = "#FFFFFF";
    public string BackgroundSecondary { get; set; } = "#F9FAFB";
    public string BackgroundTertiary { get; set; } = "#F3F4F6";
    public string TextPrimary { get; set; } = "#111827";
    public string TextSecondary { get; set; } = "#6B7280";
    public string TextTertiary { get; set; } = "#9CA3AF";
    public string Border { get; set; } = "#E5E7EB";

    // Dark Theme Overrides
    public string BackgroundPrimaryDark { get; set; } = "#0F172A";
    public string BackgroundSecondaryDark { get; set; } = "#1E293B";
    public string BackgroundTertiaryDark { get; set; } = "#334155";
    public string TextPrimaryDark { get; set; } = "#F1F5F9";
    public string TextSecondaryDark { get; set; } = "#94A3B8";
    public string TextTertiaryDark { get; set; } = "#64748B";
    public string BorderDark { get; set; } = "#334155";
}

public class TypographyTokens
{
    // Font Families
    public string FontFamilyPrimary { get; set; } = "Segoe UI Variable";
    public string FontFamilyCode { get; set; } = "Cascadia Code, Consolas, Courier New";

    // Font Sizes
    public double FontSizeHero { get; set; } = 40;
    public double FontSizeTitle { get; set; } = 28;
    public double FontSizeSubtitle { get; set; } = 20;
    public double FontSizeBody { get; set; } = 14;
    public double FontSizeCaption { get; set; } = 12;
    public double FontSizeCode { get; set; } = 13;

    // Font Weights
    public string FontWeightRegular { get; set; } = "Normal";
    public string FontWeightSemiBold { get; set; } = "SemiBold";
    public string FontWeightBold { get; set; } = "Bold";

    // Line Heights
    public double LineHeightTight { get; set; } = 1.25;
    public double LineHeightNormal { get; set; } = 1.5;
    public double LineHeightRelaxed { get; set; } = 1.75;
}

public class SpacingTokens
{
    public double XS { get; set; } = 4;
    public double S { get; set; } = 8;
    public double M { get; set; } = 12;
    public double L { get; set; } = 16;
    public double XL { get; set; } = 24;
    public double XXL { get; set; } = 32;
    public double XXXL { get; set; } = 48;
}

public class RadiusTokens
{
    public double Small { get; set; } = 4;
    public double Medium { get; set; } = 8;
    public double Large { get; set; } = 12;
    public double XLarge { get; set; } = 16;
    public double Full { get; set; } = 9999;
}

public class ShadowTokens
{
    public double ElevationSmall { get; set; } = 2;
    public double ElevationMedium { get; set; } = 4;
    public double ElevationLarge { get; set; } = 8;
    public double ElevationXLarge { get; set; } = 16;
}

public class AnimationTokens
{
    // Durations (milliseconds)
    public int DurationFast { get; set; } = 150;
    public int DurationMedium { get; set; } = 300;
    public int DurationSlow { get; set; } = 500;

    // Easing
    public string EasingDefault { get; set; } = "QuadraticEaseOut";
    public string EasingEmphasis { get; set; } = "BackEaseOut";
    public string EasingSmooth { get; set; } = "CubicEaseInOut";
}
