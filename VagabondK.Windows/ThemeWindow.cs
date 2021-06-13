using System.Windows;
using System.Windows.Media;

namespace VagabondK.Windows
{
    /// <summary>
    /// 테마가 적용된 윈도우
    /// </summary>
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
            var themeWindowHelper = new ThemeWindowHelper(this);
            SourceInitialized += themeWindowHelper.OnSourceInitialized;
            Loaded += themeWindowHelper.OnLoaded;
            Closed += themeWindowHelper.OnClosed;
        }


        /// <summary>
        /// 클라이언트 영역 배경
        /// </summary>
        public Brush ClientBackground
        {
            get { return (Brush)GetValue(ClientBackgroundProperty); }
            set { SetValue(ClientBackgroundProperty, value); }
        }

        /// <summary>
        /// 클라이언트 영역 배경 속성
        /// </summary>
        public static readonly DependencyProperty ClientBackgroundProperty =
            DependencyProperty.Register("ClientBackground", typeof(Brush), typeof(ThemeWindow), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White)));
    }
}
