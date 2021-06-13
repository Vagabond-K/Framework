using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace VagabondK.Windows
{
    class ThemeWindowHelper
    {
        public ThemeWindowHelper(Window window)
        {
            this.window = window;
        }

        private readonly Window window;

        private Window hookingOwner = null;
        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (window.Owner != null)
            {
                hookingOwner = window.Owner;
                IntPtr handle = new WindowInteropHelper(hookingOwner).Handle;
                HwndSource.FromHwnd(handle)?.AddHook(OwnerWindowProc);
            }
        }

        public void OnClosed(object sender, EventArgs e)
        {
            if (hookingOwner != null)
            {
                IntPtr handle = new WindowInteropHelper(hookingOwner).Handle;
                HwndSource.FromHwnd(handle)?.RemoveHook(OwnerWindowProc);
            }
        }

        private bool canHandled = true;
        private IntPtr OwnerWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0020:
                    if (canHandled)
                    {
                        hookingOwner.CaptureMouse();
                        hookingOwner.ReleaseMouseCapture();
                        canHandled = false;
                    }
                    else
                    {
                        canHandled = true;
                    }
                    break;
            }

            return IntPtr.Zero;
        }

        public void OnSourceInitialized(object sender, EventArgs e)
        {
            IntPtr handle = new WindowInteropHelper(window).Handle;
            HwndSource.FromHwnd(handle)?.AddHook(WindowProc);
        }

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam, (int)window.MinWidth, (int)window.MinHeight);
                    handled = true;
                    break;
            }

            return IntPtr.Zero;
        }

        [DllImport("user32")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("user32")]
        static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam, int minWidth, int minHeight)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
                mmi.ptMinTrackSize.x = minWidth;
                mmi.ptMinTrackSize.y = minHeight;
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}
