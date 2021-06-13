using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VagabondK.App.Windows
{
    /// <summary>
    /// 페이지 호스트 컨트롤
    /// </summary>
    public class PageHostControl : ContentControl
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public PageHostControl()
        {
            SetBinding(ContentProperty, nameof(PageContext.View));
            Focusable = false;
        }
        
        /// <summary>
        /// 이 System.Windows.FrameworkElement에서 종속성 속성의 유효 값이 업데이트될 때마다 호출됩니다. 변경된 특정 종속성
        /// 속성이 인수 매개 변수에서 보고됩니다. System.Windows.DependencyObject.OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs)를
        /// 재정의합니다.
        /// </summary>
        /// <param name="e">기존 값과 새 값 그리고 변경된 속성을 설명하는 이벤트 데이터입니다.</param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == ContentProperty)
            {
                if (e.NewValue is FrameworkElement frameworkElement)
                    frameworkElement.SetBinding(DataContextProperty, nameof(PageContext.ViewModel));
                else if (e.NewValue is FrameworkContentElement frameworkContentElement)
                    frameworkContentElement.SetBinding(FrameworkContentElement.DataContextProperty, nameof(PageContext.ViewModel));
            }
        }
    }
}
