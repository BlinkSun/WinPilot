using System.Globalization;
using System.Windows;
using WinPilot.Managers;
using WinPilot.Models;
using WinPilot.Views;

namespace WinPilot.Controllers;

/// <summary>
/// Centralized controller for core app-wide actions like triggering the WinPilot window.
/// </summary>
public static class AppController
{
    /// <summary>
    /// Captures the current context and opens the WinPilot popup.
    /// </summary>
    public static async void TriggerWinPilot()
    {
        ContextSnapshot context = await ContextSnapshot.Capture();
        Application.Current.Dispatcher.Invoke(() => new WinPilotWindow(context).Show());
    }
}