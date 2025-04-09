using System.Diagnostics;
using System.Text;
using WinPilot.Native;

namespace WinPilot.Helpers;

/// <summary>
/// Provides utility methods to interact with Windows UI handles.
/// </summary>
public static class WindowHelper
{
    /// <summary>
    /// Gets the title (caption) of a window.
    /// </summary>
    /// <param name="hwnd">The handle of the window.</param>
    /// <returns>The window title, or empty if unavailable.</returns>
    public static string GetWindowTitle(IntPtr hwnd)
    {
        StringBuilder buffer = new(1024);
        int length = Win32Api.GetWindowText(hwnd, buffer, buffer.Capacity);
        return length > 0 ? buffer.ToString() : string.Empty;
    }

    /// <summary>
    /// Gets the process name associated with a given window handle.
    /// </summary>
    /// <param name="hwnd">The window handle.</param>
    /// <returns>The process name (e.g., "notepad", "chrome"), or "unknown".</returns>
    public static string GetProcessName(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero)
            return "unknown";

        _ = Win32Api.GetWindowThreadProcessId(hwnd, out uint pid);

        try
        {
            Process? proc = Process.GetProcessById((int)pid);
            return proc?.ProcessName ?? "unknown";
        }
        catch
        {
            return "unknown";
        }
    }

    /// <summary>
    /// Tries to restore (unminimize) and bring a window to the foreground.
    /// </summary>
    /// <param name="hwnd">The window handle.</param>
    public static void RestoreAndFocus(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero)
            return;

        Win32Api.ShowWindow(hwnd, Win32Api.SW_RESTORE);
        Win32Api.SetForegroundWindow(hwnd);
    }
}