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

namespace VagabondK.Windows
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
            var themeWindowHelper = new ThemeWindowHelper(this);
            SourceInitialized += themeWindowHelper.OnSourceInitialized;
            Loaded += themeWindowHelper.OnLoaded;
            Closed += themeWindowHelper.OnClosed;

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
    }
}
