using System.Windows;
using WinPilot.Controllers;
using WinPilot.Managers;

namespace WinPilot;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Handles the application startup event. Registers a global hotkey to open the WinPilot window.
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
        LanguageManager.InitializeFromSettings();
        base.OnStartup(e);
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
        HotkeyManager.RegisterHotKey(
            SettingsManager.HotkeyKey,
            SettingsManager.HotkeyModifiers,
            AppController.TriggerWinPilot
        );
    }
}