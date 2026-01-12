using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LuminaNotes.Core.Services;
using LuminaNotes.WinUI.Services;
using System;
using System.Linq;
using Windows.Storage.Pickers;
using Windows.Storage;

namespace LuminaNotes.WinUI.Pages;

public sealed partial class ThemeCustomizerPage : Page
{
    private readonly ThemeService _themeService;
    private readonly ThemeApplier _themeApplier;

    public ThemeCustomizerPage()
    {
        this.InitializeComponent();
        _themeService = Ioc.Default.GetRequiredService<ThemeService>();
        _themeApplier = new ThemeApplier(_themeService);
        
        LoadCurrentTheme();
    }

    private void LoadCurrentTheme()
    {
        var tokens = _themeService.GetTokens();
        
        // Load presets
        var presets = _themeService.GetPresetNames().ToList();
        PresetsGrid.ItemsSource = presets;

        // Load current colors
        PrimaryColorPicker.Color = ParseHexColor(tokens.Colors.AccentPrimary);
        SecondaryColorPicker.Color = ParseHexColor(tokens.Colors.AccentSecondary);
        TertiaryColorPicker.Color = ParseHexColor(tokens.Colors.AccentTertiary);

        // Load typography
        TitleSizeSlider.Value = tokens.Typography.FontSizeTitle;
        BodySizeSlider.Value = tokens.Typography.FontSizeBody;
    }

    private void PresetsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (PresetsGrid.SelectedItem is string presetName)
        {
            _ = _themeService.ApplyPresetAsync(presetName);
            LoadCurrentTheme();
        }
    }

    private async void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
    {
        await _themeService.UpdateTokensAsync(tokens =>
        {
            if (sender == PrimaryColorPicker)
                tokens.Colors.AccentPrimary = ColorToHex(args.NewColor);
            else if (sender == SecondaryColorPicker)
                tokens.Colors.AccentSecondary = ColorToHex(args.NewColor);
            else if (sender == TertiaryColorPicker)
                tokens.Colors.AccentTertiary = ColorToHex(args.NewColor);
        });
    }

    private async void Typography_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        await _themeService.UpdateTokensAsync(tokens =>
        {
            tokens.Typography.FontSizeTitle = TitleSizeSlider.Value;
            tokens.Typography.FontSizeBody = BodySizeSlider.Value;
        });
    }

    private async void SpacingPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = SpacingPresetCombo.SelectedIndex;
        if (selected == 0) // Compact
            await _themeService.ApplyPresetAsync("Compact");
        else if (selected == 2) // Comfortable
            await _themeService.ApplyPresetAsync("Comfortable");
        else
            await _themeService.ResetToDefaultAsync();
    }

    private async void Apply_Click(object sender, RoutedEventArgs e)
    {
        await _themeService.SaveTokensAsync();
        ShowNotification("Theme applied successfully!");
    }

    private async void Reset_Click(object sender, RoutedEventArgs e)
    {
        await _themeService.ResetToDefaultAsync();
        LoadCurrentTheme();
        ShowNotification("Theme reset to default");
    }

    private async void Export_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var json = _themeService.ExportTheme();
            
            var savePicker = new FileSavePicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);
            
            savePicker.SuggestedFileName = "theme";
            savePicker.FileTypeChoices.Add("JSON", new[] { ".json" });
            
            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                await FileIO.WriteTextAsync(file, json);
                ShowNotification("Theme exported successfully!");
            }
        }
        catch (Exception ex)
        {
            ShowNotification($"Export failed: {ex.Message}");
        }
    }

    private async void Import_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var openPicker = new FileOpenPicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);
            
            openPicker.FileTypeFilter.Add(".json");
            
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var json = await FileIO.ReadTextAsync(file);
                var success = await _themeService.ImportThemeAsync(json);
                
                if (success)
                {
                    LoadCurrentTheme();
                    ShowNotification("Theme imported successfully!");
                }
                else
                {
                    ShowNotification("Invalid theme file");
                }
            }
        }
        catch (Exception ex)
        {
            ShowNotification($"Import failed: {ex.Message}");
        }
    }

    private void ShowNotification(string message)
    {
        // TODO: Implement notification display
        System.Diagnostics.Debug.WriteLine(message);
    }

    private Windows.UI.Color ParseHexColor(string hex)
    {
        hex = hex.TrimStart('#');
        return Windows.UI.Color.FromArgb(
            255,
            Convert.ToByte(hex.Substring(0, 2), 16),
            Convert.ToByte(hex.Substring(2, 2), 16),
            Convert.ToByte(hex.Substring(4, 2), 16)
        );
    }

    private string ColorToHex(Windows.UI.Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }
}
