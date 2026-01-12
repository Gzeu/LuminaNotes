# 🎨 Design Tokens System

## Overview

LuminaNotes uses a **design tokens** system for centralized, scalable, and customizable theming. Design tokens are the atomic values (colors, spacing, typography) that define the visual design language.

## 📦 What Are Design Tokens?

Design tokens are named entities that store visual design attributes. They enable:

- **Consistency**: Single source of truth for design values
- **Scalability**: Easy to maintain across large apps
- **Customization**: Users can personalize themes
- **Cross-platform**: Export tokens to CSS, JSON, etc.

## 🎨 Token Categories

### 1. Color Tokens

```csharp
public class ColorTokens
{
    // Accent Colors
    public string AccentPrimary { get; set; } = "#6366F1";
    public string AccentSecondary { get; set; } = "#8B5CF6";
    public string AccentTertiary { get; set; } = "#EC4899";

    // Semantic Colors
    public string Success { get; set; } = "#10B981";
    public string Warning { get; set; } = "#F59E0B";
    public string Error { get; set; } = "#EF4444";
    public string Info { get; set; } = "#3B82F6";
}
```

### 2. Typography Tokens

```csharp
public class TypographyTokens
{
    public string FontFamilyPrimary { get; set; } = "Segoe UI Variable";
    public string FontFamilyCode { get; set; } = "Cascadia Code";
    
    public double FontSizeHero { get; set; } = 40;
    public double FontSizeTitle { get; set; } = 28;
    public double FontSizeBody { get; set; } = 14;
}
```

### 3. Spacing Tokens

```csharp
public class SpacingTokens
{
    public double XS { get; set; } = 4;     // 4px
    public double S { get; set; } = 8;      // 8px
    public double M { get; set; } = 12;     // 12px
    public double L { get; set; } = 16;     // 16px
    public double XL { get; set; } = 24;    // 24px
    public double XXL { get; set; } = 32;   // 32px
}
```

### 4. Radius Tokens

```csharp
public class RadiusTokens
{
    public double Small { get; set; } = 4;   // Chips
    public double Medium { get; set; } = 8;  // Buttons
    public double Large { get; set; } = 12;  // Cards
    public double Full { get; set; } = 9999; // Pills
}
```

### 5. Shadow Tokens

```csharp
public class ShadowTokens
{
    public double ElevationSmall { get; set; } = 2;
    public double ElevationMedium { get; set; } = 4;
    public double ElevationLarge { get; set; } = 8;
}
```

### 6. Animation Tokens

```csharp
public class AnimationTokens
{
    public int DurationFast { get; set; } = 150;    // ms
    public int DurationMedium { get; set; } = 300;  // ms
    public int DurationSlow { get; set; } = 500;    // ms
    
    public string EasingDefault { get; set; } = "QuadraticEaseOut";
}
```

## 🔧 Using Design Tokens

### In Code (C#)

```csharp
using LuminaNotes.Core.Services;

public class MyViewModel
{
    private readonly ThemeService _themeService;

    public MyViewModel(ThemeService themeService)
    {
        _themeService = themeService;
        var tokens = _themeService.GetTokens();
        
        // Access token values
        var primaryColor = tokens.Colors.AccentPrimary;
        var titleSize = tokens.Typography.FontSizeTitle;
    }
}
```

### In XAML

```xml
<!-- Colors -->
<Border Background="{StaticResource AccentPrimaryBrush}"/>

<!-- Typography -->
<TextBlock FontSize="{StaticResource FontSizeTitle}"/>

<!-- Spacing -->
<StackPanel Spacing="{StaticResource SpacingM}"/>

<!-- Radius -->
<Border CornerRadius="{StaticResource CardCornerRadius}"/>
```

## 🎭 Built-in Theme Presets

### Color Themes

| Preset | Primary | Secondary | Tertiary | Use Case |
|--------|---------|-----------|----------|----------|
| **Default** | #6366F1 (Indigo) | #8B5CF6 (Purple) | #EC4899 (Pink) | Modern, vibrant |
| **Ocean** | #0EA5E9 (Sky) | #06B6D4 (Cyan) | #3B82F6 (Blue) | Calm, professional |
| **Forest** | #10B981 (Green) | #14B8A6 (Teal) | #22C55E (Lime) | Natural, fresh |
| **Sunset** | #F59E0B (Amber) | #F97316 (Orange) | #EF4444 (Red) | Warm, energetic |
| **Midnight** | #8B5CF6 (Purple) | #A855F7 (Fuchsia) | #D946EF (Magenta) | Dark, mysterious |
| **Monochrome** | #374151 (Gray) | #4B5563 | #6B7280 | Minimalist, neutral |

### Density Presets

| Preset | Description | Spacing Scale | Font Sizes |
|--------|-------------|---------------|------------|
| **Compact** | Dense UI, max content | 2-4-8-12-16-24 | 11-13-18-24-32 |
| **Default** | Balanced | 4-8-12-16-24-32 | 12-14-20-28-40 |
| **Comfortable** | Spacious, relaxed | 6-12-16-20-32-48 | 14-16-22-32-48 |

## 🔄 Theme Management

### Apply a Preset

```csharp
await _themeService.ApplyPresetAsync("Ocean");
```

### Custom Theme

```csharp
await _themeService.UpdateTokensAsync(tokens =>
{
    tokens.Colors.AccentPrimary = "#FF6B6B";
    tokens.Spacing.M = 16;
    tokens.Typography.FontSizeBody = 15;
});
```

### Export Theme

```csharp
var json = _themeService.ExportTheme();
// Save to file or share
```

### Import Theme

```csharp
var json = await File.ReadAllTextAsync("my-theme.json");
var success = await _themeService.ImportThemeAsync(json);
```

## 🎨 Theme Customizer UI

Navigate to **Settings → Theme Customizer** to:

1. **Choose Presets**: Select from 8 built-in themes
2. **Customize Colors**: Pick accent colors with ColorPicker
3. **Adjust Typography**: Slider for font sizes
4. **Set Density**: Compact/Default/Comfortable
5. **Live Preview**: See changes in real-time
6. **Export/Import**: Share themes with others

### Accessing Theme Customizer

```csharp
// In MainWindow or Settings
ContentFrame.Navigate(typeof(ThemeCustomizerPage));
```

## 📁 Token Storage

Tokens are stored as JSON at:
```
%LOCALAPPDATA%\LuminaNotes\design-tokens.json
```

### Sample JSON

```json
{
  "Colors": {
    "AccentPrimary": "#6366F1",
    "AccentSecondary": "#8B5CF6",
    "AccentTertiary": "#EC4899",
    "Success": "#10B981",
    "Warning": "#F59E0B",
    "Error": "#EF4444"
  },
  "Typography": {
    "FontFamilyPrimary": "Segoe UI Variable",
    "FontSizeHero": 40,
    "FontSizeTitle": 28,
    "FontSizeBody": 14
  },
  "Spacing": {
    "XS": 4,
    "S": 8,
    "M": 12,
    "L": 16,
    "XL": 24,
    "XXL": 32
  }
}
```

## 🔔 Theme Change Events

### Subscribe to Changes

```csharp
public class MyService
{
    public MyService(ThemeService themeService)
    {
        themeService.ThemeChanged += OnThemeChanged;
    }

    private void OnThemeChanged(object? sender, DesignTokens tokens)
    {
        // React to theme changes
        Console.WriteLine($"New primary color: {tokens.Colors.AccentPrimary}");
    }
}
```

## 🎯 Best Practices

### ✅ DO

- **Use semantic names**: `AccentPrimary` not `BlueColor`
- **Reference tokens**: Use `{StaticResource}` in XAML
- **Test themes**: Check Light/Dark and all presets
- **Document custom tokens**: Add comments for custom values
- **Version control**: Commit `design-tokens.json` for team sync

### ❌ DON'T

- **Hardcode values**: Always use tokens
- **Mix systems**: Don't combine tokens with magic numbers
- **Break accessibility**: Ensure contrast ratios with custom colors
- **Over-customize**: Stick to preset guidelines

## 🚀 Advanced Usage

### Dynamic Theme Switching

```csharp
public async Task SwitchToThemeAsync(string themeName)
{
    await _themeService.ApplyPresetAsync(themeName);
    
    // Animate transition
    await UIHelper.FadeOutAsync(mainContent, 150);
    // Apply tokens (handled automatically by ThemeApplier)
    await UIHelper.FadeIn(mainContent, 150);
}
```

### Theme Generator

```csharp
public DesignTokens GenerateThemeFromColor(string primaryHex)
{
    var tokens = new DesignTokens();
    
    // Generate complementary colors
    tokens.Colors.AccentPrimary = primaryHex;
    tokens.Colors.AccentSecondary = ShiftHue(primaryHex, 30);
    tokens.Colors.AccentTertiary = ShiftHue(primaryHex, 60);
    
    return tokens;
}
```

### Conditional Tokens

```csharp
// Different tokens for different contexts
var tokens = _themeService.GetTokens();
var isDarkTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark;

var bgColor = isDarkTheme 
    ? tokens.Colors.BackgroundPrimaryDark 
    : tokens.Colors.BackgroundPrimary;
```

## 📊 Token Reference

### Complete Token Map

```
Colors (17 tokens)
├── AccentPrimary, AccentSecondary, AccentTertiary
├── Success, Warning, Error, Info
├── BackgroundPrimary, BackgroundSecondary, BackgroundTertiary
├── TextPrimary, TextSecondary, TextTertiary
├── Border
└── Dark variants (7 tokens)

Typography (12 tokens)
├── FontFamilyPrimary, FontFamilyCode
├── FontSizeHero, Title, Subtitle, Body, Caption, Code
├── FontWeightRegular, SemiBold, Bold
└── LineHeightTight, Normal, Relaxed

Spacing (7 tokens)
└── XS, S, M, L, XL, XXL, XXXL

Radius (5 tokens)
└── Small, Medium, Large, XLarge, Full

Shadows (4 tokens)
└── ElevationSmall, Medium, Large, XLarge

Animations (6 tokens)
├── DurationFast, Medium, Slow
└── EasingDefault, Emphasis, Smooth
```

## 🔗 Resources

- [Design Tokens W3C Spec](https://design-tokens.github.io/community-group/format/)
- [Material Design Tokens](https://m3.material.io/foundations/design-tokens)
- [Figma Tokens Plugin](https://www.figma.com/community/plugin/843461159747178978/Figma-Tokens)

---

*Design Tokens System v1.0 - January 2026*
