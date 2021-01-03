using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace VagabondK
{
    /// <summary>
    /// 테마 리소스 사전
    /// </summary>
    public class ThemeResourceDictionary : ResourceDictionary
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public ThemeResourceDictionary()
        {
            ThemeName = "Dark";
        }

        private string themeName = null;

        /// <summary>
        /// 테마 이름
        /// </summary>
        public string ThemeName
        {
            get => themeName;
            set
            {
                if (themeName != value)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        Source = new Uri($"pack://application:,,,/VagabondK.Windows;component/Themes/{value}.xaml", UriKind.RelativeOrAbsolute);
                    }
                    themeName = value;
                }
            }
        }
    }
}
