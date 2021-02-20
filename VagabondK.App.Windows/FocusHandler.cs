using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace VagabondK
{
    /// <summary>
    /// 포커스 핸들러
    /// </summary>
    [ServiceDescription(typeof(IFocusHandler))]
    public class FocusHandler : IFocusHandler
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="pageContext">페이지 컨텍스트</param>
        public FocusHandler(PageContext pageContext)
        {
            this.pageContext = pageContext;
        }

        private readonly PageContext pageContext;

        /// <summary>
        /// 포커스 설정
        /// </summary>
        /// <param name="name">포커스를 설정할 컨트롤 또는 속성 이름</param>
        /// <returns>포커스 설정 결과</returns>
        public bool Focus(string name)
        {
            var result = false;

            if (pageContext.View is FrameworkElement frameworkElement)
            {
                result = frameworkElement?.FindName(name) is UIElement target && (target.IsFocused || target.Focus());
                if (!result)
                {
                    foreach (var control in GetAllChilds(frameworkElement))
                    {
                        BindingExpression binding = null;
                        if (control is TextBox textBox)
                            binding = textBox.GetBindingExpression(TextBox.TextProperty);
                        else if (control is ToggleButton toggleButton)
                            binding = toggleButton.GetBindingExpression(ToggleButton.IsCheckedProperty);
                        else if (control is Selector selector)
                            binding = selector.GetBindingExpression(Selector.SelectedItemProperty);
                        else if (control is RangeBase range)
                            binding = range.GetBindingExpression(RangeBase.ValueProperty);

                        result = binding?.ResolvedSourcePropertyName == name && (control.IsFocused || control.Focus());
                    }
                }
            }

            return result;
        }

        private static IEnumerable<Control> GetAllChilds(DependencyObject root)
        {
            Queue<DependencyObject> queue = new Queue<DependencyObject>();
            DependencyObject dependencyObject;
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                dependencyObject = queue.Dequeue();
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    var child = VisualTreeHelper.GetChild(dependencyObject, i);
                    queue.Enqueue(child);
                    if (child is Control childControl)
                    {
                        yield return childControl;
                    }
                }
            }
        }

    }
}
