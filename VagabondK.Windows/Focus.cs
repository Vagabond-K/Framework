using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VagabondK.Windows
{
    /// <summary>
    /// 화면을 로드할 때 기본으로 포커스를 설정하기 위한 확장 속성
    /// </summary>
    public static class Focus
    {
        /// <summary>
        /// 화면을 로드할 때 기본으로 포커스를 설정할지 여부를 가져옵니다.
        /// </summary>
        /// <param name="obj">대상 개체</param>
        /// <returns>기본 포커스 여부</returns>
        public static bool GetIsDefault(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDefaultProperty);
        }

        /// <summary>
        /// 화면을 로드할 때 기본으로 포커스를 설정할지 여부를 지정합니다.
        /// </summary>
        /// <param name="obj">대상 개체</param>
        /// <param name="value">기본 포커스 여부</param>
        public static void SetIsDefault(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDefaultProperty, value);
        }

        /// <summary>
        /// 화면을 로드할 때 기본으로 포커스를 설정하기 위한 확장 속성
        /// </summary>
        ///
        /// <AttachedPropertyComments>
        /// <summary>
        /// 화면을 로드할 때 기본으로 포커스를 설정할지 여부를 지정하거나 가져옵니다.
        /// </summary>
        /// <value>기본값은 false입니다.</value>
        /// </AttachedPropertyComments>
        public static readonly DependencyProperty IsDefaultProperty =
            DependencyProperty.RegisterAttached("IsDefault", typeof(bool), typeof(Focus), new PropertyMetadata(false,
                (sender, e) =>
                {
                    if (sender is FrameworkElement element && !element.IsLoaded && (bool)e.NewValue)
                        element.Focus();
                }));
    }
}
