# 🎨 LuminaNotes Design System

## Overview

LuminaNotes uses a modern design system based on **Fluent Design 3.0** with custom enhancements for a premium note-taking experience.

## 🎨 Color Palette

### Primary Colors

```xml
<Color x:Key="AccentPrimary">#6366F1</Color>      <!-- Indigo -->
<Color x:Key="AccentSecondary">#8B5CF6</Color>    <!-- Purple -->
<Color x:Key="AccentTertiary">#EC4899</Color>     <!-- Pink -->
```

### Semantic Colors

```xml
<Color x:Key="SuccessColor">#10B981</Color>       <!-- Green -->
<Color x:Key="WarningColor">#F59E0B</Color>       <!-- Amber -->
<Color x:Key="ErrorColor">#EF4444</Color>         <!-- Red -->
```

### Usage

- **AccentPrimary**: Main interactive elements (buttons, links)
- **AccentSecondary**: Hover states, secondary actions
- **AccentTertiary**: Highlights, special emphasis
- **Gradient**: Hero sections, premium features

## 📊 Typography

### Text Styles

| Style | Font Size | Weight | Usage |
|-------|-----------|--------|-------|
| **HeroText** | 40px | Bold | Landing pages, major headings |
| **PageTitle** | 28px | SemiBold | Page headers |
| **SectionTitle** | 20px | SemiBold | Section headers |
| **Body** | 14px | Regular | Main content |
| **Caption** | 12px | Regular | Secondary info, timestamps |
| **Code** | 13px | Monospace | Code snippets, technical text |

### Font Families

- **Primary**: Segoe UI Variable (Windows 11 default)
- **Code**: Cascadia Code, Consolas, Courier New

## 📍 Spacing System

```xml
XS:  4px   <!-- Tight spacing -->
S:   8px   <!-- Compact -->
M:   12px  <!-- Default -->
L:   16px  <!-- Comfortable -->
XL:  24px  <!-- Generous -->
XXL: 32px  <!-- Large gaps -->
```

### Usage Guidelines

- Use **XS-S** for inline elements (tags, chips)
- Use **M-L** for card padding, list spacing
- Use **XL-XXL** for section separation

## 📎 Corner Radius

```xml
Small:   4px   <!-- Chips, badges -->
Control: 8px   <!-- Buttons, inputs -->
Card:    12px  <!-- Cards, panels -->
```

## 🌟 Elevation & Shadows

### Levels

1. **Level 1** (2px): Subtle lift for cards on hover
2. **Level 2** (4px): Default elevated cards
3. **Level 3** (8px): Modals, popovers

### Implementation

```xml
<Border Shadow="{ThemeShadow}" Translation="0,0,4"/>
```

## ⏱️ Animation Timing

### Durations

- **Fast**: 150ms - Micro-interactions (hover, press)
- **Medium**: 300ms - Standard transitions (fade, slide)
- **Slow**: 500ms - Complex animations (page transitions)

### Easing Functions

- **QuadraticEase (EaseOut)**: Default for most animations
- **BackEase**: Playful, emphasis (success states)
- **CubicEase**: Smooth, natural motion

## 🧩 Components

### Buttons

#### Accent Button
```xml
<Button Content="Save" Style="{StaticResource AccentButtonStyle}"/>
```
- Use for primary actions
- High contrast, prominent

#### Icon Button
```xml
<Button Style="{StaticResource IconButtonStyle}">
    <FontIcon Glyph="&#xE710;"/>
</Button>
```
- Use for toolbar actions
- 40x40px touch target

#### Card Button
```xml
<Button Style="{StaticResource CardButtonStyle}">
    <!-- Content -->
</Button>
```
- Use for list items, selectable cards

### Cards

#### Base Card
```xml
<Border Style="{StaticResource BaseCardStyle}">
    <!-- Content -->
</Border>
```

#### Elevated Card
```xml
<Border Style="{StaticResource ElevatedCardStyle}">
    <!-- Content with shadow -->
</Border>
```

#### Accent Card
```xml
<Border Style="{StaticResource AccentCardStyle}">
    <!-- Premium content with gradient -->
</Border>
```

### Custom Controls

#### AnimatedCard
```xml
<controls:AnimatedCard CardContent="{Binding}">
    <StackPanel>
        <TextBlock Text="Title"/>
        <TextBlock Text="Content"/>
    </StackPanel>
</controls:AnimatedCard>
```
- Auto hover/press animations
- Elevation effects

#### NoteCard
```xml
<controls:NoteCard 
    Title="My Note"
    ContentPreviewText="Preview..."
    IsPinned="True"
    DateString="2h ago"
    Tags="{Binding TagsList}"/>
```
- Reusable note display
- Badges for pinned/encrypted

#### RichEditorControl
```xml
<controls:RichEditorControl 
    Text="{Binding Content, Mode=TwoWay}"/>
```
- Markdown toolbar
- Live word count
- Preview toggle

#### LoadingIndicator
```xml
<controls:LoadingIndicator Message="Loading notes..."/>
```

#### EmptyState
```xml
<controls:EmptyState 
    Icon="&#xE74C;"
    Title="No notes yet"
    Description="Create your first note to get started"
    ActionButtonText="Create Note"
    ActionButtonClicked="CreateNote_Click"/>
```

## 🎥 Animation Examples

### Fade In
```csharp
UIHelper.FadeIn(myElement, durationMs: 300);
```

### Slide Up
```csharp
UIHelper.SlideInFromBottom(myCard, distance: 50, durationMs: 400);
```

### Success Animation
```csharp
await UIHelper.ShowSuccessAsync(saveButton);
```

## 📦 Value Converters

### BoolToVisibilityConverter
```xml
Visibility="{Binding IsVisible, Converter={StaticResource BoolToVisibilityConverter}}"
```

### DateTimeToStringConverter
```xml
Text="{Binding Created, Converter={StaticResource DateTimeToStringConverter}}"
<!-- Outputs: "2h ago", "3d ago", "Jan 12" -->
```

### CountToVisibilityConverter
```xml
Visibility="{Binding Notes.Count, Converter={StaticResource CountToVisibilityConverter}}"
<!-- Show only if count > 0 -->
```

## ♻️ Best Practices

### DO

✅ Use semantic colors (Success, Warning, Error)
✅ Maintain consistent spacing
✅ Animate state changes (hover, press)
✅ Use empty states for better UX
✅ Test on both Light and Dark themes

### DON'T

❌ Mix custom colors with theme colors randomly
❌ Use hardcoded spacing values
❌ Create animations longer than 500ms
❌ Forget accessibility (contrast ratios)
❌ Ignore touch targets (min 40x40px)

## 🎨 Theming

### Dark Theme (Default)
```csharp
RequestedTheme = ApplicationTheme.Dark;
```

### Light Theme
```csharp
RequestedTheme = ApplicationTheme.Light;
```

### Mica/Acrylic Backgrounds
- **Mica**: Transparent material for main window (Windows 11+)
- **Acrylic**: Frosted glass fallback (Windows 10)

Automatically applied in `App.xaml.cs`.

## 📄 Resources

- [Fluent Design System](https://fluent2.microsoft.design/)
- [Windows App SDK Docs](https://learn.microsoft.com/windows/apps/)
- [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery)

---

*Design system v1.0 - January 2026*
