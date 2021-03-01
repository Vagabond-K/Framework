using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Interop;
using System.Windows.Media;

namespace VagabondK
{
    /// <summary>
    /// 테마가 적용된 Ribbon 윈도우
    /// </summary>
    public class ThemeRibbonWindow : RibbonWindow
    {
        static ThemeRibbonWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemeRibbonWindow), new FrameworkPropertyMetadata(typeof(ThemeRibbonWindow)));
            WindowStyleProperty.OverrideMetadata(typeof(ThemeRibbonWindow), new FrameworkPropertyMetadata(WindowStyle.None, OnWindowStyleChanged));
            AllowsTransparencyProperty.OverrideMetadata(typeof(ThemeRibbonWindow), new FrameworkPropertyMetadata(true, OnAllowsTransparencyChanged));
        }

        private static void OnWindowStyleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ThemeRibbonWindow window && e.NewValue is WindowStyle windowStyle && windowStyle != WindowStyle.None)
                window.WindowStyle = WindowStyle.None;
        }

        private static void OnAllowsTransparencyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ThemeRibbonWindow window && e.NewValue is bool allowsTransparency && !allowsTransparency)
                window.AllowsTransparency = true;
        }


        /// <summary>
        /// 생성자
        /// </summary>
        public ThemeRibbonWindow()
        {
            SourceInitialized += Window_SourceInitialized;
            UpdateActualWindowCaptionButtonWidth();
        }

        /// <summary>
        /// 이 System.Windows.FrameworkElement에서 종속성 속성의 유효 값이 업데이트될 때마다 호출됩니다. 변경된 특정 종속성 속성이 인수 매개 변수에서 보고됩니다. System.Windows.DependencyObject.OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs)를 재정의합니다.
        /// </summary>
        /// <param name="e">기존 값과 새 값 그리고 변경된 속성을 설명하는 이벤트 데이터입니다.</param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ResizeModeProperty || e.Property == WindowStateProperty)
                UpdateActualWindowCaptionButtonWidth();
        }

        private void UpdateActualWindowCaptionButtonWidth()
        {
            switch (ResizeMode)
            {
                case ResizeMode.NoResize:
                    ActualWindowCaptionButtonWidth = WindowState == WindowState.Maximized ? 47 : 44;
                    break;
                case ResizeMode.CanMinimize:
                    ActualWindowCaptionButtonWidth = 44 + (WindowState == WindowState.Maximized ? 47 : 44);
                    break;
                case ResizeMode.CanResize:
                case ResizeMode.CanResizeWithGrip:
                    ActualWindowCaptionButtonWidth = 88 + (WindowState == WindowState.Maximized ? 47 : 44);
                    break;
            }
        }

        /// <summary>
        /// 실제 적용된 윈도우 버튼 전체 너비
        /// </summary>
        public double ActualWindowCaptionButtonWidth
        {
            get { return (double)GetValue(ActualWindowCaptionButtonWidthProperty); }
            private set { SetValue(ActualWindowCaptionButtonWidthProperty, value); }
        }

        /// <summary>
        /// 실제 적용된 윈도우 버튼 전체 너비 속성 정의
        /// </summary>
        public static readonly DependencyProperty ActualWindowCaptionButtonWidthProperty =
            DependencyProperty.Register("ActualWindowCaptionButtonWidth", typeof(double), typeof(ThemeRibbonWindow), new PropertyMetadata(double.NaN));

        /// <summary>
        /// 클라이언트 영역 배경
        /// </summary>
        public Brush ClientBackground
        {
            get { return (Brush)GetValue(ClientBackgroundProperty); }
            set { SetValue(ClientBackgroundProperty, value); }
        }

        /// <summary>
        /// 클라이언트 영역 배경 속성 정의
        /// </summary>
        public static readonly DependencyProperty ClientBackgroundProperty =
            DependencyProperty.Register("ClientBackground", typeof(Brush), typeof(ThemeRibbonWindow), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White)));



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
