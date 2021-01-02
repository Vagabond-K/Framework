using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;

namespace VagabondK
{
    /// <summary>
    /// 테마가 적용된 윈도우
    /// </summary>
    [TemplatePart(Name= "PART_MinimizeButton", Type= typeof(Button))]
    [TemplatePart(Name = "PART_MaximizeButton", Type= typeof(Button))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class ThemeWindow : Window
    {
        static ThemeWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemeWindow), new FrameworkPropertyMetadata(typeof(ThemeWindow)));
            WindowStyleProperty.OverrideMetadata(typeof(ThemeWindow), new FrameworkPropertyMetadata(WindowStyle.None, OnWindowStyleChanged));
            AllowsTransparencyProperty.OverrideMetadata(typeof(ThemeWindow), new FrameworkPropertyMetadata(true, OnAllowsTransparencyChanged));
        }

        private static void OnWindowStyleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ThemeWindow window && e.NewValue is WindowStyle windowStyle && windowStyle != WindowStyle.None)
                window.WindowStyle = WindowStyle.None;
        }

        private static void OnAllowsTransparencyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ThemeWindow window && e.NewValue is bool allowsTransparency && !allowsTransparency)
                window.AllowsTransparency = true;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public ThemeWindow()
        {
            SourceInitialized += Window_SourceInitialized;
        }

        private Button minimizeButton;
        private Button maximizeButton;
        private Button closeButton;

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal
        /// processes call System.Windows.FrameworkElement.ApplyTemplate.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template.FindName("PART_MinimizeButton", this) is Button minimizeButton)
            {
                this.minimizeButton = minimizeButton;
                this.minimizeButton.Click += Minimize_Click;
            }
            if (Template.FindName("PART_MaximizeButton", this) is Button maximizeButton)
            {
                this.maximizeButton = maximizeButton;
                this.maximizeButton.Click += Maximize_Click;
            }
            if (Template.FindName("PART_CloseButton", this) is Button closeButton)
            {
                this.closeButton = closeButton;
                this.closeButton.Click += Close_Click;
            }
        }

        void Window_SourceInitialized(object sender, EventArgs e)
        {
            IntPtr handle = new WindowInteropHelper(this).Handle;
            HwndSource.FromHwnd(handle)?.AddHook(WindowProc);
        }

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam, (int)MinWidth, (int)MinHeight);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    break;
                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    break;
            }
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
