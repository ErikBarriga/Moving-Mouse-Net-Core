using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MouseCursorMovement
{
    public static class WinUser32Dll
    {
        [DllImport("user32.dll")]
        internal static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        internal static extern bool SetCursorPos(Point lpPoint);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

        // Activate an application window.
        //[DllImport("user32.DLL")]
        //internal static extern bool SetForegroundWindow(IntPtr hWnd);
        //[DllImport("user32.DLL", CharSet = CharSet.Unicode)]
        //private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        // Get a handle to the Calculator application. The window class and window name were obtained using the Spy++ tool.
        //IntPtr calculatorHandle = WinUser32Dll.FindWindow("CalcFrame", "Calculator");
        // Verify that Calculator is a running process.
        /*              if (calculatorHandle == IntPtr.Zero)
                        {
                            MessageBox.Show("Calculator is not running.");
                            return;
                        }
                        // Make Calculator the foreground application and send it a set of calculations.
                        WinUser32Dll.SetForegroundWindow(calculatorHandle);
        */


        internal static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        internal static void SendKey(int asciiCode)
        {
            const int KEYEVENTF_KEYUP = 0x0002;
             keybd_event((byte)asciiCode, 0, 0, IntPtr.Zero);
            keybd_event((byte)asciiCode, 0, KEYEVENTF_KEYUP, IntPtr.Zero);
        }

    }
}
