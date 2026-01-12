using LuminaNotes.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LuminaNotes.Core.Services;

/// <summary>
/// Manages design tokens and theme switching
/// </summary>
public class ThemeService
{
    private readonly string _tokensPath;
    private DesignTokens _currentTokens;
    private readonly Dictionary<string, DesignTokens> _presets;

    public event EventHandler<DesignTokens>? ThemeChanged;

    public ThemeService()
    {
        _tokensPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "LuminaNotes",
            "design-tokens.json"
        );

        _currentTokens = new DesignTokens();
        _presets = InitializePresets();
    }

    /// <summary>
    /// Get current design tokens
    /// </summary>
    public DesignTokens GetTokens() => _currentTokens;

    /// <summary>
    /// Load tokens from file or create default
    /// </summary>
    public async Task LoadTokensAsync()
    {
        try
        {
            if (File.Exists(_tokensPath))
            {
                var json = await File.ReadAllTextAsync(_tokensPath);
                _currentTokens = JsonSerializer.Deserialize<DesignTokens>(json) ?? new DesignTokens();
            }
            else
            {
                _currentTokens = new DesignTokens();
                await SaveTokensAsync();
            }
        }
        catch
        {
            _currentTokens = new DesignTokens();
        }

        ThemeChanged?.Invoke(this, _currentTokens);
    }

    /// <summary>
    /// Save current tokens to file
    /// </summary>
    public async Task SaveTokensAsync()
    {
        try
        {
            var dir = Path.GetDirectoryName(_tokensPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var json = JsonSerializer.Serialize(_currentTokens, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(_tokensPath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save tokens: {ex.Message}");
        }
    }

    /// <summary>
    /// Update specific token values
    /// </summary>
    public async Task UpdateTokensAsync(Action<DesignTokens> updateAction)
    {
        updateAction(_currentTokens);
        await SaveTokensAsync();
        ThemeChanged?.Invoke(this, _currentTokens);
    }

    /// <summary>
    /// Apply a preset theme
    /// </summary>
    public async Task ApplyPresetAsync(string presetName)
    {
        if (_presets.TryGetValue(presetName, out var preset))
        {
            _currentTokens = preset;
            await SaveTokensAsync();
            ThemeChanged?.Invoke(this, _currentTokens);
        }
    }

    /// <summary>
    /// Get available preset names
    /// </summary>
    public IEnumerable<string> GetPresetNames() => _presets.Keys;

    /// <summary>
    /// Reset to default theme
    /// </summary>
    public async Task ResetToDefaultAsync()
    {
        await ApplyPresetAsync("Default");
    }

    /// <summary>
    /// Initialize built-in theme presets
    /// </summary>
    private Dictionary<string, DesignTokens> InitializePresets()
    {
        return new Dictionary<string, DesignTokens>
        {
            ["Default"] = new DesignTokens(),
            
            ["Ocean"] = new DesignTokens
            {
                Colors = new ColorTokens
                {
                    AccentPrimary = "#0EA5E9",
                    AccentSecondary = "#06B6D4",
                    AccentTertiary = "#3B82F6"
                }
            },

            ["Forest"] = new DesignTokens
            {
                Colors = new ColorTokens
                {
                    AccentPrimary = "#10B981",
                    AccentSecondary = "#14B8A6",
                    AccentTertiary = "#22C55E"
                }
            },

            ["Sunset"] = new DesignTokens
            {
                Colors = new ColorTokens
                {
                    AccentPrimary = "#F59E0B",
                    AccentSecondary = "#F97316",
                    AccentTertiary = "#EF4444"
                }
            },

            ["Midnight"] = new DesignTokens
            {
                Colors = new ColorTokens
                {
                    AccentPrimary = "#8B5CF6",
                    AccentSecondary = "#A855F7",
                    AccentTertiary = "#D946EF"
                }
            },

            ["Monochrome"] = new DesignTokens
            {
                Colors = new ColorTokens
                {
                    AccentPrimary = "#374151",
                    AccentSecondary = "#4B5563",
                    AccentTertiary = "#6B7280"
                }
            },

            ["Compact"] = new DesignTokens
            {
                Spacing = new SpacingTokens
                {
                    XS = 2,
                    S = 4,
                    M = 8,
                    L = 12,
                    XL = 16,
                    XXL = 24,
                    XXXL = 32
                },
                Typography = new TypographyTokens
                {
                    FontSizeHero = 32,
                    FontSizeTitle = 24,
                    FontSizeSubtitle = 18,
                    FontSizeBody = 13,
                    FontSizeCaption = 11
                }
            },

            ["Comfortable"] = new DesignTokens
            {
                Spacing = new SpacingTokens
                {
                    XS = 6,
                    S = 12,
                    M = 16,
                    L = 20,
                    XL = 32,
                    XXL = 48,
                    XXXL = 64
                },
                Typography = new TypographyTokens
                {
                    FontSizeHero = 48,
                    FontSizeTitle = 32,
                    FontSizeSubtitle = 22,
                    FontSizeBody = 16,
                    FontSizeCaption = 14
                }
            }
        };
    }

    /// <summary>
    /// Export current theme to JSON string
    /// </summary>
    public string ExportTheme()
    {
        return JsonSerializer.Serialize(_currentTokens, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    /// <summary>
    /// Import theme from JSON string
    /// </summary>
    public async Task<bool> ImportThemeAsync(string json)
    {
        try
        {
            var tokens = JsonSerializer.Deserialize<DesignTokens>(json);
            if (tokens != null)
            {
                _currentTokens = tokens;
                await SaveTokensAsync();
                ThemeChanged?.Invoke(this, _currentTokens);
                return true;
            }
        }
        catch
        {
            // Invalid JSON
        }
        return false;
    }
}
