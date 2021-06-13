using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace VagabondK.Windows.Converters
{
    /// <summary>
    /// if else문과 같이 순차적으로 논리값을 확인하여 결과를 반환
    /// </summary>
    [ContentProperty(nameof(Results))]
    public class IfElseConverter : IMultiValueConverter
    {
        /// <summary>
        /// 결과 리스트. MultiBinding 개수에 맞추어야 함.
        /// </summary>
        public ArrayList Results { get; set; } = new ArrayList();

        /// <summary>
        /// 소스 값을 바인딩 대상의 값으로 변환합니다. 데이터 바인딩 엔진이 소스 바인딩에서 바인딩 대상으로 값을 전파할 때 이 메서드를 호출합니다.
        /// </summary>
        /// <param name="values">System.Windows.Data.MultiBinding의 소스 바인딩에서 생성하는 값의 배열입니다.
        /// System.Windows.DependencyProperty.UnsetValue 값은 변환에 제공할 값이 소스 바인딩에 없음을 나타냅니다.</param>
        /// <param name="targetType">바인딩 대상 속성의 형식입니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>변환된 값입니다.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return DependencyProperty.UnsetValue;

            int index = 0;
            foreach (var value in values)
            {
                if (value == null || value == DependencyProperty.UnsetValue || value == Binding.DoNothing
                    || !Helper.IsBooleanConvertable(value)) continue;

                if (value.To<bool>())
                {
                    return Results.Count > index ? Results[index] : DependencyProperty.UnsetValue;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// 바인딩 대상 값을 소스 바인딩 값으로 변환합니다.
        /// </summary>
        /// <param name="value">바인딩 대상에서 생성하는 값입니다.</param>
        /// <param name="targetTypes">변환할 형식의 배열입니다. 배열 길이는 메서드에서 반환하도록 제안되는 값의 개수와 형식을 나타냅니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>대상 값에서 소스 값으로 다시 변환된 값의 배열입니다.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)

        {
            throw new NotImplementedException();
        }
    }
}
