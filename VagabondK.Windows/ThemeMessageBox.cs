using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace VagabondK.Windows
{
    /// <summary>
    /// 테마가 적용된 메시지 상자
    /// </summary>
    public class ThemeMessageBox
    {
        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBoxText">메시지</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(string messageBoxText)
            => Show(messageBoxText, string.Empty);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(string messageBoxText, string caption)
            => Show(messageBoxText, caption, MessageBoxButton.OK);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
            => Show(messageBoxText, caption, button, MessageBoxImage.None);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <param name="icon">아이콘</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
            => Show(messageBoxText, caption, button, icon, MessageBoxResult.None);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <param name="icon">아이콘</param>
        /// <param name="defaultResult">기본 선택 메시지 상자 결과</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
            => Show(null, messageBoxText, caption, button, icon, defaultResult);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="owner">소유자 창</param>
        /// <param name="messageBoxText">메시지</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(Window owner, string messageBoxText)
            => Show(owner, messageBoxText, string.Empty);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="owner">소유자 창</param>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption)
            => Show(owner, messageBoxText, caption, MessageBoxButton.OK);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="owner">소유자 창</param>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
            => Show(owner, messageBoxText, caption, button, MessageBoxImage.None);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="owner">소유자 창</param>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <param name="icon">아이콘</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
            => Show(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="owner">소유자 창</param>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <param name="icon">아이콘</param>
        /// <param name="defaultResult">기본 선택 메시지 상자 결과</param>
        /// <returns>메시지 상자 결과</returns>
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            if (owner == null)
                owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive) ?? Application.Current.MainWindow;

            if (owner?.IsLoaded != true)
                owner = null;

            var content = new ThemeMessageBoxContentControl
            {
                Content = messageBoxText,
                button = button,
                Icon = icon,
                result = defaultResult,
            };

            var messageBoxWindow = new ThemeWindow()
            {
                ResizeMode = ResizeMode.NoResize,
                Owner = owner,
                ShowInTaskbar = owner == null,
                WindowStartupLocation = owner == null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                Title = caption,
                Content = content
            };

            if (owner != null)
                messageBoxWindow.SetBinding(Window.IconProperty, new Binding(nameof(owner.Icon)) { Source = owner });

            content.window = messageBoxWindow;

            switch (icon)
            {
                case MessageBoxImage.Hand:
                    SystemSounds.Hand.Play();
                    break;
                case MessageBoxImage.Asterisk:
                    SystemSounds.Asterisk.Play();
                    break;
                case MessageBoxImage.Question:
                    SystemSounds.Question.Play();
                    break;
                case MessageBoxImage.Warning:
                    SystemSounds.Exclamation.Play();
                    break;
            }

            messageBoxWindow.KeyDown += (s, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Escape)
                {
                    switch (button)
                    {
                        case MessageBoxButton.OK:
                            content.result = MessageBoxResult.OK;
                            break;
                        case MessageBoxButton.OKCancel:
                        case MessageBoxButton.YesNo:
                        case MessageBoxButton.YesNoCancel:
                            content.result = MessageBoxResult.Cancel;
                            break;
                    }
                    messageBoxWindow.Close();
                }
            };

            messageBoxWindow.ShowDialog();
            return content.result;
        }
    }

    /// <summary>
    /// 테마가 적용된 메시지 상자 내용 컨트롤
    /// </summary>
    [TemplatePart(Name = "PART_Button1", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Button2", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Button3", Type = typeof(Button))]
    public class ThemeMessageBoxContentControl : ContentControl
    {
        static ThemeMessageBoxContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemeMessageBoxContentControl), new FrameworkPropertyMetadata(typeof(ThemeMessageBoxContentControl)));
        }

        private Button button1;
        private Button button2;
        private Button button3;

        internal ThemeWindow window;
        internal MessageBoxButton button;
        internal MessageBoxResult result;

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal
        /// processes call System.Windows.FrameworkElement.ApplyTemplate.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template.FindName("PART_Button1", this) is Button button1)
            {
                this.button1 = button1;
                this.button1.Click += OnButton1Click;

                switch (button)
                {
                    case MessageBoxButton.OK:
                        this.button1.Content = Windows.Resources.ThemeMessageBox_Button_OK_Text;
                        this.button1.Focus();
                        break;
                    case MessageBoxButton.OKCancel:
                        this.button1.Content = Windows.Resources.ThemeMessageBox_Button_OK_Text;
                        if (result == MessageBoxResult.OK || result == MessageBoxResult.None)
                            this.button1.Focus();
                        break;
                    case MessageBoxButton.YesNo:
                    case MessageBoxButton.YesNoCancel:
                        this.button1.Content = Windows.Resources.ThemeMessageBox_Button_Yes_Text;
                        if (result == MessageBoxResult.Yes || result == MessageBoxResult.None)
                            this.button1.Focus();
                        break;
                }
            }
            if (Template.FindName("PART_Button2", this) is Button button2)
            {
                this.button2 = button2;
                this.button2.Click += OnButton2Click;

                switch (button)
                {
                    case MessageBoxButton.OK:
                        this.button2.Visibility = Visibility.Collapsed;
                        break;
                    case MessageBoxButton.OKCancel:
                        this.button2.Content = Windows.Resources.ThemeMessageBox_Button_Cancel_Text;
                        if (result == MessageBoxResult.Cancel)
                            this.button2.Focus();
                        break;
                    case MessageBoxButton.YesNo:
                    case MessageBoxButton.YesNoCancel:
                        this.button2.Content = Windows.Resources.ThemeMessageBox_Button_No_Text;
                        if (result == MessageBoxResult.No)
                            this.button2.Focus();
                        break;
                }
            }
            if (Template.FindName("PART_Button3", this) is Button button3)
            {
                this.button3 = button3;
                this.button3.Click += OnButton3Click;

                switch (button)
                {
                    case MessageBoxButton.OK:
                    case MessageBoxButton.OKCancel:
                    case MessageBoxButton.YesNo:
                        this.button3.Visibility = Visibility.Collapsed;
                        break;
                    case MessageBoxButton.YesNoCancel:
                        this.button3.Content = Windows.Resources.ThemeMessageBox_Button_Cancel_Text;
                        if (result == MessageBoxResult.Cancel)
                            this.button3.Focus();
                        break;
                }
            }
        }

        private void OnButton1Click(object sender, RoutedEventArgs e)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                case MessageBoxButton.OKCancel:
                    result = MessageBoxResult.OK;
                    window.Close();
                    break;
                case MessageBoxButton.YesNo:
                case MessageBoxButton.YesNoCancel:
                    result = MessageBoxResult.Yes;
                    window.Close();
                    break;
            }
        }
        private void OnButton2Click(object sender, RoutedEventArgs e)
        {
            switch (button)
            {
                case MessageBoxButton.OKCancel:
                    result = MessageBoxResult.Cancel;
                    window.Close();
                    break;
                case MessageBoxButton.YesNo:
                case MessageBoxButton.YesNoCancel:
                    result = MessageBoxResult.No;
                    window.Close();
                    break;
            }
        }
        private void OnButton3Click(object sender, RoutedEventArgs e)
        {
            switch (button)
            {
                case MessageBoxButton.YesNoCancel:
                    result = MessageBoxResult.Cancel;
                    window.Close();
                    break;
            }
        }

        /// <summary>
        /// 메시지 상자 아이콘
        /// </summary>
        public MessageBoxImage Icon
        {
            get { return (MessageBoxImage)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// 메시지 상자 아이콘 속성 정의
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(MessageBoxImage), typeof(ThemeMessageBoxContentControl), new PropertyMetadata(MessageBoxImage.None));
    }
}
