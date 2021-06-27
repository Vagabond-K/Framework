using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using VagabondK.Windows;

namespace VagabondK.App.Windows
{
    /// <summary>
    /// 대화상자 서비스
    /// </summary>
    [ServiceDescription(typeof(IDialog))]
    public class Dialog : IDialog
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        public Dialog(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public Task<bool?> ShowDialog(Type viewModelType, Type viewType, string title, Action<object, object> initializer, out object viewModel)
        {
            using (var pageScope = serviceProvider.CreatePageScope(viewModelType, viewType, title))
            {
                var pageContext = pageScope.ServiceProvider.GetService<PageContext>();
                initializer?.Invoke(pageContext.ViewModel, pageContext.View);

                var owner = pageContext.Owner?.View is DependencyObject dependencyObject ? Window.GetWindow(dependencyObject) : Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive) ?? Application.Current.MainWindow;

                if (owner?.IsLoaded != true)
                    owner = null;

                if (!(pageContext.View is Window dialogWindow))
                {
                    dialogWindow = new ThemeWindow
                    {
                        SizeToContent = SizeToContent.WidthAndHeight
                    };

                    dialogWindow.SetBinding(Window.TitleProperty, nameof(pageContext.Title));
                    dialogWindow.SetBinding(ContentControl.ContentProperty, nameof(pageContext.View));
                    dialogWindow.SetBinding(Window.ResizeModeProperty, new Binding { Path = new PropertyPath($"{nameof(pageContext.View)}.(0)", ResizeModeProperty) });
                    dialogWindow.SetBinding(Window.ShowInTaskbarProperty, new Binding { Path = new PropertyPath($"{nameof(pageContext.View)}.(0)", ShowInTaskbarProperty) });

                    if (owner != null)
                        dialogWindow.SetBinding(Window.IconProperty, new Binding(nameof(owner.Icon)) { Source = owner });

                    (pageContext.View as FrameworkElement)?.SetBinding(FrameworkElement.DataContextProperty, nameof(pageContext.ViewModel));
                    (pageContext.View as FrameworkContentElement)?.SetBinding(FrameworkContentElement.DataContextProperty, nameof(pageContext.ViewModel));

                    dialogWindow.DataContext = pageContext;
                }
                else
                {
                    dialogWindow.DataContext = pageContext.ViewModel;
                }

                if (dialogWindow != owner)
                    dialogWindow.Owner = owner;
                else
                    owner = null;

                dialogWindow.WindowStartupLocation = owner == null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
                pageContext.Result = null;

                void OnViewScopePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    if (e.PropertyName == nameof(PageContext.Result) && pageContext.Result != null)
                    {
                        dialogWindow.DialogResult = pageContext.Result;
                    }
                }

                void OnLoaded(object sender, RoutedEventArgs e)
                {
                    (pageContext.ViewModel as INotifyLoaded)?.OnLoaded();
                }

                void OnSourceInitialized(object sender, EventArgs e)
                {
                    if (pageContext.View != dialogWindow)
                    {
                        if (pageContext.View is FrameworkElement frameworkElement)
                        {
                            if (!double.IsNaN(frameworkElement.Width))
                            {
                                dialogWindow.Width = dialogWindow.ActualWidth;
                                frameworkElement.ClearValue(FrameworkElement.WidthProperty);
                            }
                            if (!double.IsNaN(frameworkElement.Height))
                            {
                                dialogWindow.Height = dialogWindow.ActualHeight;
                                frameworkElement.ClearValue(FrameworkElement.HeightProperty);
                            }

                            var point1 = frameworkElement.TranslatePoint(new Point(0, 0), dialogWindow);
                            var point2 = frameworkElement.TranslatePoint(new Point(frameworkElement.ActualWidth, frameworkElement.ActualHeight), dialogWindow);

                            var borderWidth = point1.X + (dialogWindow.ActualWidth - point2.X);
                            var borderHeight = point1.Y + (dialogWindow.ActualHeight - point2.Y);

                            dialogWindow.MinWidth = frameworkElement.MinWidth + borderWidth;
                            dialogWindow.MinHeight = frameworkElement.MinHeight + borderHeight;
                            if (!double.IsInfinity(frameworkElement.MaxWidth))
                                dialogWindow.MaxWidth = frameworkElement.MaxWidth + borderWidth;
                            if (!double.IsInfinity(frameworkElement.MaxHeight))
                                dialogWindow.MaxHeight = frameworkElement.MaxHeight + borderHeight;

                            frameworkElement.MinWidth = 0;
                            frameworkElement.MinHeight = 0;
                            frameworkElement.MaxWidth = double.PositiveInfinity;
                            frameworkElement.MaxHeight = double.PositiveInfinity;
                        }
                    }

                    if (dialogWindow.ResizeMode == ResizeMode.CanResize
                        || dialogWindow.ResizeMode == ResizeMode.CanResizeWithGrip)
                        dialogWindow.SizeToContent = SizeToContent.Manual;
                }

                async void Closing(object sender, System.ComponentModel.CancelEventArgs e)
                {
                    if (pageContext.ViewModel is IQueryClosing queryClosing)
                        e.Cancel = await queryClosing.QueryClosing(dialogWindow.DialogResult ?? false) == false;
                }

                void OnClosed(object sender, EventArgs e)
                {
                    pageContext.PropertyChanged -= OnViewScopePropertyChanged;
                    dialogWindow.Loaded -= OnLoaded;
                    dialogWindow.SourceInitialized -= OnSourceInitialized;
                    dialogWindow.Closed -= OnClosed;
                    dialogWindow.Closing -= Closing;

                    if (pageContext.Result == null)
                        pageContext.Result = dialogWindow.DialogResult;
                }

                pageContext.PropertyChanged += OnViewScopePropertyChanged;
                dialogWindow.Loaded += OnLoaded;
                dialogWindow.SourceInitialized += OnSourceInitialized;
                dialogWindow.Closing += Closing;
                dialogWindow.Closed += OnClosed;

                viewModel = pageContext.ViewModel;

                return Task.FromResult(dialogWindow.ShowDialog());
            }
        }




        /// <summary>
        /// 대화상자 ResizeMode 가져오기
        /// </summary>
        /// <param name="obj">대화상자 뷰</param>
        /// <returns>ResizeMode</returns>
        public static ResizeMode GetResizeMode(DependencyObject obj)
        {
            return (ResizeMode)obj.GetValue(ResizeModeProperty);
        }

        /// <summary>
        /// 대화상자 ResizeMode 설정하기
        /// </summary>
        /// <param name="obj">대화상자 뷰</param>
        /// <param name="value">ResizeMode</param>
        public static void SetResizeMode(DependencyObject obj, ResizeMode value)
        {
            obj.SetValue(ResizeModeProperty, value);
        }

        /// <summary>
        /// 대화상자 뷰의 ResizeMode 확장 속성
        /// </summary>
        ///
        /// <AttachedPropertyComments>
        /// <summary>
        /// 대화상자의 ResizeMode를 설정하거나 가져옵니다.
        /// </summary>
        /// <value>기본값은 CanResize입니다.</value>
        /// </AttachedPropertyComments>
        public static readonly DependencyProperty ResizeModeProperty =
            DependencyProperty.RegisterAttached("ResizeMode", typeof(ResizeMode), typeof(Dialog), new PropertyMetadata(ResizeMode.CanResize));



        /// <summary>
        /// 대화상자의 작업표시줄 표시 여부 가져오기
        /// </summary>
        /// <param name="obj">대화상자 뷰</param>
        /// <returns>대화상자의 작업표시줄 표시 여부</returns>
        public static bool GetShowInTaskbar(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowInTaskbarProperty);
        }

        /// <summary>
        /// 대화상자의 작업표시줄 표시 여부 설정하기
        /// </summary>
        /// <param name="obj">대화상자 뷰</param>
        /// <param name="value">대화상자의 작업표시줄 표시 여부</param>
        public static void SetShowInTaskbar(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowInTaskbarProperty, value);
        }

        /// <summary>
        /// 대화상자의 작업표시줄 표시 여부 확장 속성
        /// </summary>
        ///
        /// <AttachedPropertyComments>
        /// <summary>
        /// 대화상자의 작업표시줄 표시 여부를 설정하거나 가져옵니다.
        /// </summary>
        /// <value>기본값은 true입니다.</value>
        /// </AttachedPropertyComments>
        public static readonly DependencyProperty ShowInTaskbarProperty =
            DependencyProperty.RegisterAttached("ShowInTaskbar", typeof(bool), typeof(Dialog), new PropertyMetadata(true));


        /// <summary>
        /// 대화상자에 표시된 버튼의 OK 버튼 여부 가져오기
        /// </summary>
        /// <param name="obj">대화상자 내부의 버튼</param>
        /// <returns>OK 버튼 여부</returns>
        public static bool GetIsOk(Button obj) => (bool)obj.GetValue(IsOkProperty);

        /// <summary>
        /// 대화상자에 표시된 버튼의 OK 버튼 여부 설정하기
        /// </summary>
        /// <param name="obj">대화상자 내부의 버튼</param>
        /// <param name="value">OK 버튼 여부</param>
        public static void SetIsOk(Button obj, bool value) => obj.SetValue(IsOkProperty, value);

        /// <summary>
        /// 대화상자에 표시된 버튼의 OK 버튼 여부 확장 속성
        /// </summary>
        ///
        /// <AttachedPropertyComments>
        /// <summary>
        /// 대화상자에 표시된 버튼의 OK 버튼 여부를 설정하거나 가져옵니다.
        /// </summary>
        /// <value>기본값은 false입니다.</value>
        /// </AttachedPropertyComments>
        public static readonly DependencyProperty IsOkProperty =
            DependencyProperty.RegisterAttached("IsOk", typeof(bool), typeof(Dialog), new PropertyMetadata(false, OnIsOkChanged));

        private static void OnIsOkChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is Button button
                && eventArgs.NewValue is bool newValue)
            {
                if (newValue)
                    button.Click += OkButtonClick;
                else
                    button.Click -= OkButtonClick;
            }
        }

        private static void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var window = Window.GetWindow(button);
                if (window != null)
                {
                    try
                    {
                        window.DialogResult = true;
                    }
                    catch { }
                }
            }
        }

    }
}
