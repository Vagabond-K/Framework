using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace VagabondK
{
    /// <summary>
    /// 테마 관리자
    /// </summary>
    public static class ThemeManager
    {
        /// <summary>
        /// 응용프로그램 테마 변경 이벤트
        /// </summary>
        public static event EventHandler AppThemeChanged;

        private static string appThemeName = null;

        /// <summary>
        /// 응용프로그램 테마 이름을 설정하거나 가져옵니다.
        /// </summary>
        public static string AppThemeName
        {
            get => appThemeName;
            set
            {
                var app = Application.Current;
                if (app != null && appThemeName != value)
                {
                    if (app.Resources == null)
                        app.Resources = new ResourceDictionary();

                    MargeThemeResourceDictionary(app.Resources, appThemeName, value);
                    appThemeName = value;
                    AppThemeChanged?.Invoke(app, EventArgs.Empty);
                }
            }
        }

        private static void MargeThemeResourceDictionary(ResourceDictionary target, string oldThemeName, string newThemeName)
        {
            if (oldThemeName != null
                && themeResources.TryGetValue(oldThemeName, out var oldTheme))
                foreach (var removeTheme in target.MergedDictionaries.Where(d => d == oldTheme).ToArray())
                    target.MergedDictionaries.Remove(removeTheme);

            if (newThemeName != null)
            {
                if (!themeResources.TryGetValue(newThemeName, out var newTheme))
                {
                    try
                    {
                        newTheme = new ResourceDictionary { Source = new Uri($"pack://application:,,,/VagabondK.Windows;component/Themes/{newThemeName}.xaml", UriKind.RelativeOrAbsolute) };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Invalid theme name.", ex);
                    }
                    themeResources[newThemeName] = newTheme;
                }
                target.MergedDictionaries.Add(newTheme);
            }
        }


        private static Dictionary<string, ResourceDictionary> themeResources = new Dictionary<string, ResourceDictionary>();

        /// <summary>
        /// FrameworkElement의 테마 이름을 가져옵니다.
        /// </summary>
        /// <param name="obj">FrameworkElement</param>
        /// <returns>테마 이름</returns>
        public static string GetThemeName(FrameworkElement obj)
        {
            return (string)obj.GetValue(ThemeNameProperty);
        }

        /// <summary>
        /// FrameworkElement의 테마 이름을 설정합니다.
        /// </summary>
        /// <param name="obj">FrameworkElement</param>
        /// <param name="value">테마 이름</param>
        public static void SetThemeName(FrameworkElement obj, string value)
        {
            obj.SetValue(ThemeNameProperty, value);
        }

        /// <summary>
        /// FrameworkElement의 테마 이름 Attached 속성 정의입니다.
        /// </summary>
        public static readonly DependencyProperty ThemeNameProperty =
            DependencyProperty.RegisterAttached("ThemeName", typeof(string), typeof(ThemeManager), new FrameworkPropertyMetadata(null, OnThemeNameChanged));

        private static void OnThemeNameChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is FrameworkElement frameworkElement)
            {
                if (frameworkElement.Resources == null)
                    frameworkElement.Resources = new ResourceDictionary();

                MargeThemeResourceDictionary(frameworkElement.Resources, e.OldValue as string, e.NewValue as string);
            }
        }
    }
}
