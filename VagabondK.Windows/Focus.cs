using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VagabondK
{
    /// <summary>
    /// 기본 포커스를 위한 확장 속성
    /// </summary>
    public static class Focus
    {
        /// <summary>
        /// 기본 포커스인지 여부를 가져옵니다.
        /// </summary>
        /// <param name="obj">대상 개체</param>
        /// <returns>기본 포커스 여부</returns>
        public static bool GetIsDefault(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDefaultProperty);
        }

        /// <summary>
        /// 기본 포커스인지 여부를 설정합니다.
        /// </summary>
        /// <param name="obj">대상 개체</param>
        /// <param name="value">기본 포커스 여부</param>
        public static void SetIsDefault(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDefaultProperty, value);
        }

        /// <summary>
        /// 기본 포커스를 위한 확장 속성
        /// </summary>
        public static readonly DependencyProperty IsDefaultProperty =
            DependencyProperty.RegisterAttached("IsDefault", typeof(bool), typeof(Focus), new PropertyMetadata(false,
                (sender, e) =>
                {
                    if (sender is FrameworkElement element && !element.IsLoaded && (bool)e.NewValue)
                        element.Focus();
                }));
    }
}
