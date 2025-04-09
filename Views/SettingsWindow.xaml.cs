using System.ComponentModel;
using System.Windows;
using WinPilot.Controllers;
using WinPilot.Managers;
using WinPilot.ViewModels;

namespace WinPilot.Views;

/// <summary>
/// Logique d'interaction pour SettingsWindow.xaml
/// </summary>
public partial class SettingsWindow : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Handles the Click event of the Save button.
    /// </summary>
    private void OnCloseClicked(object sender, RoutedEventArgs e)
    {
        SettingsManager.Save();
        HotkeyManager.RebindHotKey(
            SettingsManager.HotkeyKey,
            SettingsManager.HotkeyModifiers,
            AppController.TriggerWinPilot
        );
        LanguageManager.ApplyLanguage(SettingsManager.Language);
        Close();
    }

    /// <summary>
    /// Handles the Click event of the Shutdown button.
    /// </summary>
    private void OnShutdownClicked(object sender, RoutedEventArgs e)
    {
        AppController.ShutdownApp();
    }

    /// <summary>
    /// Handles the close event of the window.
    /// </summary>
    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        if (!IsKeyValidFromDataContext())
            AppController.ShutdownApp();
    }

    /// <summary>
    /// Checks if the API key is valid from the data context.
    /// </summary>
    private bool IsKeyValidFromDataContext()
    {
        if (DataContext is SettingsViewModel vm)
        {
            return vm.IsApiKeyValid;
        }
        return false;
    }
}
