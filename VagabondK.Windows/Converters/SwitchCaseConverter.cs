using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace VagabondK.Converters
{
    /// <summary>
    /// switch case문과 같은 동작의 결과로 변환
    /// </summary>
    [ContentProperty(nameof(Map))]
    public class SwitchCaseConverter : IValueConverter
    {
        /// <summary>
        /// 각 case별 맵
        /// </summary>
        public Collection<SwitchCaseConverterItem> Map { get; set; } = new Collection<SwitchCaseConverterItem>();

        /// <summary>
        /// 값을 변환합니다.
        /// </summary>
        /// <param name="value">바인딩 소스에서 생성한 값입니다.</param>
        /// <param name="targetType">바인딩 대상 속성의 형식입니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>변환된 값입니다.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var item in Map)
            {
                if (Equals(item.Case, value))
                {
                    return item.Value;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// 값을 변환합니다.
        /// </summary>
        /// <param name="value">바인딩 대상에서 생성한 값입니다.</param>
        /// <param name="targetType">변환할 대상 형식입니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>변환된 값입니다.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// SwitchCaseConverter의 Map 요소
    /// </summary>
    public class SwitchCaseConverterItem
    {
        /// <summary>
        /// case
        /// </summary>
        public object Case { get; set; }

        /// <summary>
        /// case에 해당하는 값
        /// </summary>
        public object Value { get; set; }
    }
}
