using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Extensions.DependencyInjection;
using LuminaNotes.Core.Services;
using LuminaNotes.WinUI.ViewModels;
using System;

namespace LuminaNotes.WinUI;

public partial class App : Application
{
    private Window? m_window;
    public static IServiceProvider Services { get; private set; } = null!;

    public App()
    {
        InitializeComponent();
        ConfigureServices();
        ConfigureBackdrop();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();

        // Register Core services
        services.AddSingleton<DatabaseService>();
        services.AddSingleton<NoteService>();
        services.AddSingleton<AiService>();
        services.AddSingleton<GraphService>();

        // Register ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<DailyNotesViewModel>();
        services.AddTransient<AllNotesViewModel>();
        services.AddTransient<GraphViewModel>();
        services.AddTransient<SearchViewModel>();
        services.AddTransient<SettingsViewModel>();

        Services = services.BuildServiceProvider();

        // Initialize database
        var dbService = Services.GetRequiredService<DatabaseService>();
        _ = dbService.InitializeAsync();
    }

    private void ConfigureBackdrop()
    {
        // Setup Mica/Acrylic backdrop with fallback
        if (MicaController.IsSupported())
        {
            SystemBackdrop = new MicaBackdrop { Kind = MicaKind.BaseAlt };
        }
        else
        {
            SystemBackdrop = new DesktopAcrylicBackdrop();
        }

        // Dark theme by default
        RequestedTheme = ApplicationTheme.Dark;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();
    }
}
