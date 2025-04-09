using WinPilot.Native;

namespace WinPilot.Helpers;

/// <summary>
/// Provides methods to simulate copy/paste keyboard shortcuts.
/// </summary>
public static class CopyPasteHelper
{
    /// <summary>
    /// Simulates Ctrl+C.
    /// </summary>
    public static void Copy()
    {
        Win32Api.keybd_event(Win32Api.VK_CONTROL, 0, Win32Api.KEYEVENTF_EXTENDEDKEY, 0);
        Win32Api.keybd_event(Win32Api.VK_C, 0, Win32Api.KEYEVENTF_EXTENDEDKEY, 0);
        Win32Api.keybd_event(Win32Api.VK_C, 0, Win32Api.KEYEVENTF_EXTENDEDKEY | Win32Api.KEYEVENTF_KEYUP, 0);
        Win32Api.keybd_event(Win32Api.VK_CONTROL, 0, Win32Api.KEYEVENTF_EXTENDEDKEY | Win32Api.KEYEVENTF_KEYUP, 0);
    }

    /// <summary>
    /// Simulates Ctrl+V.
    /// </summary>
    public static void Paste()
    {
        Win32Api.keybd_event(Win32Api.VK_CONTROL, 0, Win32Api.KEYEVENTF_EXTENDEDKEY, 0);
        Win32Api.keybd_event(Win32Api.VK_V, 0, Win32Api.KEYEVENTF_EXTENDEDKEY, 0);
        Win32Api.keybd_event(Win32Api.VK_V, 0, Win32Api.KEYEVENTF_EXTENDEDKEY | Win32Api.KEYEVENTF_KEYUP, 0);
        Win32Api.keybd_event(Win32Api.VK_CONTROL, 0, Win32Api.KEYEVENTF_EXTENDEDKEY | Win32Api.KEYEVENTF_KEYUP, 0);
    }
}