using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Animation;
using WinPilot.Helpers;
using WinPilot.Models;
using WinPilot.Native;
using WinPilot.ViewModels;
using WinPilot.Views;

namespace WinPilot.Controllers;

/// <summary>
/// Provides utility methods to handle shared UI logic such as animations and window positioning.
/// </summary>
public static class UIController
{
    //────────────────────────────────────────────
    #region 🧠 Application Window Helpers
    //────────────────────────────────────────────

    /// <summary>
    /// Shows the WinPilot window with the specified context snapshot.
    /// </summary>
    public static void ShowWinPilotWindow(ContextSnapshot context)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            new WinPilotWindow(context).Show();
        });
    }

    /// <summary>
    /// Shows the settings window with the current settings context.
    /// </summary>
    public static void ShowSettingsWindow()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            SettingsWindow settingsWindow = new()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                DataContext = new SettingsViewModel()
            };
            settingsWindow.ShowDialog();
        });
    }

    /// <summary>
    /// Closes the WinPilot window with a fade-out animation.
    /// </summary>
    public static async Task CloseWinPilotWindowAsync()
    {
        await Application.Current.Dispatcher.InvokeAsync(async () =>
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w is WinPilotWindow window)
                {
                    FadeOut(window);
                    await Task.Delay(250);
                    window.Close();
                    break;
                }
            }
        });
    }

    //────────────────────────────────────────────
    #endregion

    //────────────────────────────────────────────
    #region ✨ Window Effects
    //────────────────────────────────────────────

    /// <summary>
    /// Fades in the specified window with a short animation.
    /// </summary>
    public static void FadeIn(Window window)
    {
        DoubleAnimation fadeIn = new(0, 1, new Duration(TimeSpan.FromMilliseconds(250)));
        window.BeginAnimation(Window.OpacityProperty, fadeIn);
    }

    /// <summary>
    /// Fades out the specified window with a short animation.
    /// </summary>
    public static void FadeOut(Window window)
    {
        DoubleAnimation fadeOut = new(1, 0, new Duration(TimeSpan.FromMilliseconds(250)));
        window.BeginAnimation(Window.OpacityProperty, fadeOut);
    }

    /// <summary>
    /// Positions the window near the cursor, within screen bounds.
    /// </summary>
    public static void PositionWindowNearCursor(Window window, double offsetY = 10, double fallbackHeight = 300)
    {
        Point cursor = CursorHelper.GetCursorPosition();
        IntPtr monitor = Win32Api.MonitorFromPoint(new Win32Api.POINT((int)cursor.X, (int)cursor.Y), Win32Api.MONITOR_DEFAULTTONEAREST);

        Win32Api.MONITORINFO monitorInfo = new() { cbSize = Marshal.SizeOf(typeof(Win32Api.MONITORINFO)) };
        Win32Api.GetMonitorInfo(monitor, ref monitorInfo);

        double screenLeft = monitorInfo.rcWork.Left;
        double screenTop = monitorInfo.rcWork.Top;
        double screenRight = monitorInfo.rcWork.Right;
        double screenBottom = monitorInfo.rcWork.Bottom;

        double targetLeft = cursor.X - (window.Width / 2);
        double targetTop = cursor.Y + offsetY;

        if (targetLeft + window.Width > screenRight)
            targetLeft = screenRight - window.Width;
        if (targetLeft < screenLeft)
            targetLeft = screenLeft;

        if (targetTop + fallbackHeight > screenBottom)
            targetTop = screenBottom - fallbackHeight;
        if (targetTop < screenTop)
            targetTop = screenTop;

        window.Left = targetLeft;
        window.Top = targetTop;
    }
    //────────────────────────────────────────────
    #endregion
}
