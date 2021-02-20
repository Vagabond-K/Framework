using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VagabondK
{
    public static class Focus
    {
        public static bool GetIsDefault(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDefaultProperty);
        }

        public static void SetIsDefault(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDefaultProperty, value);
        }

        public static readonly DependencyProperty IsDefaultProperty =
            DependencyProperty.RegisterAttached("IsDefault", typeof(bool), typeof(Focus), new PropertyMetadata(false,
                (sender, e) =>
                {
                    if (sender is FrameworkElement element && !element.IsLoaded && (bool)e.NewValue)
                        element.Focus();
                }));
    }
}
