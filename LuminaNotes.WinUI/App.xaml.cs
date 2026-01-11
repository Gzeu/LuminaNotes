using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using LuminaNotes.Core.Services;
using LuminaNotes.WinUI.ViewModels;
using System;

namespace LuminaNotes.WinUI;

public partial class App : Application
{
    private Window? m_window;

    public App()
    {
        this.InitializeComponent();
        ConfigureDependencyInjection();
        ConfigureSystemBackdrop();
    }

    private void ConfigureDependencyInjection()
    {
        var services = new ServiceCollection();

        // Core services
        services.AddSingleton<DatabaseService>();
        services.AddSingleton<NoteService>();
        services.AddSingleton<AiService>();
        services.AddSingleton<GraphService>();

        // ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<DailyNotesViewModel>();
        services.AddTransient<AllNotesViewModel>();
        services.AddTransient<GraphViewModel>();
        services.AddTransient<SearchViewModel>();
        services.AddTransient<SettingsViewModel>();

        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
    }

    private void ConfigureSystemBackdrop()
    {
        // Setup Mica/Acrylic backdrop
        if (MicaController.IsSupported())
        {
            SystemBackdrop = new MicaBackdrop { Kind = MicaKind.BaseAlt };
        }
        else
        {
            SystemBackdrop = new DesktopAcrylicBackdrop();
        }

        // Theme default: Dark
        RequestedTheme = ApplicationTheme.Dark;
    }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        // Initialize database
        var dbService = Ioc.Default.GetRequiredService<DatabaseService>();
        await dbService.InitializeAsync();

        m_window = new MainWindow();
        m_window.Activate();
    }
}
