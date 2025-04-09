using System.Runtime.InteropServices;
using System.Text;

namespace WinPilot.Native;

/// <summary>
/// Centralized declarations of native Win32 APIs, constants, and structs used throughout the application.
/// </summary>
public static class Win32Api
{
    //───────────────────────────────────────────────
    #region ⌨️ Keyboard Simulation (SendInput / keybd_event)
    //───────────────────────────────────────────────

    /// <summary>
    /// Simulates a low-level keyboard event (legacy alternative to SendInput).
    /// </summary>
    [DllImport("user32.dll", SetLastError = true)]
    public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, nuint dwExtraInfo);

    public const int KEYEVENTF_EXTENDEDKEY = 0x1;
    public const int KEYEVENTF_KEYUP = 0x2;

    public const byte VK_CONTROL = 0x11;
    public const byte VK_V = 0x56;
    public const byte VK_C = 0x43;

    //───────────────────────────────────────────────
    #endregion

    //───────────────────────────────────────────────
    #region 🖱️ Cursor / Mouse
    //───────────────────────────────────────────────

    /// <summary>
    /// Retrieves the current position of the cursor, in screen coordinates.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT(int x, int y)
    {
        public int X = x;
        public int Y = y;
    }

    //───────────────────────────────────────────────
    #endregion

    //───────────────────────────────────────────────
    #region 🖼 Window Capture / Position
    //───────────────────────────────────────────────

    /// <summary>
    /// Retrieves the dimensions of the bounding rectangle of the specified window.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hwnd, out RECT rect);

    /// <summary>
    /// Copies the contents of a window into a device context (used for screenshots).
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdc, int flags);

    /// <summary>
    /// RECT structure used to define the dimensions of a window.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public readonly int Width => Right - Left;
        public readonly int Height => Bottom - Top;
    }

    //───────────────────────────────────────────────
    #endregion

    //───────────────────────────────────────────────
    #region 🔥 Global Hotkeys
    //───────────────────────────────────────────────

    public const int HOTKEY_ID = 9000;
    public const int WM_HOTKEY = 0x0312;

    /// <summary>
    /// Registers a system-wide hotkey.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

    /// <summary>
    /// Unregisters a system-wide hotkey.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr hwnd, int id);

    //───────────────────────────────────────────────
    #endregion

    //───────────────────────────────────────────────
    #region 🧠 Window Focus / Title
    //───────────────────────────────────────────────
    public const int SW_RESTORE = 9;

    /// <summary>
    /// Restore the specified window.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    /// <summary>
    /// Retrieves a handle to the foreground window (the window with which the user is currently interacting).
    /// </summary>
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    /// <summary>
    /// Brings the specified window to the foreground.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hwnd);

    /// <summary>
    /// Copies the text of the specified window’s title bar into a buffer.
    /// </summary>
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int nMaxCount);

    /// <summary>
    /// Retrieves the identifier of the thread that created the specified window, and optionally the process ID.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);

    //───────────────────────────────────────────────
    #endregion

    //───────────────────────────────────────────────
    #region 🖥 Monitor / Screen Informations
    //───────────────────────────────────────────────
    public const uint MONITOR_DEFAULTTONEAREST = 2;

    /// <summary>
    /// Retrieves a handle to the display monitor that is nearest to the specified point.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern IntPtr MonitorFromPoint(POINT pt, uint dwFlags);

    /// <summary>
    /// Retrieves information about the specified display monitor.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    /// <summary>
    /// Retrieves the monitor information for the current cursor position.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MONITORINFO
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }

    //───────────────────────────────────────────────
    #endregion
}