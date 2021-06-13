using System;
using System.Collections.Generic;
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
    /// 수치값의 부호에 따른 변환
    /// </summary>
    public class NumericSignToObjectConverter : MarkupExtension, IMultiValueConverter, IValueConverter
    {
        /// <summary>
        /// 양수일 경우의 결과
        /// </summary>
        public object Positive { get; set; }

        /// <summary>
        /// 음수일 경우의 결과
        /// </summary>
        public object Negative { get; set; }

        /// <summary>
        /// 0일 경우의 결과
        /// </summary>
        public object Zero { get; set; }

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
            if (values == null || values.Length == 0) return DependencyProperty.UnsetValue;

            return Convert(values.Sum(value => value.To<decimal>()));
        }

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
            if (value == null || value == DependencyProperty.UnsetValue || value == Binding.DoNothing) return value;
            if (value == DBNull.Value) return null;

            return Convert(value.To<decimal>());
        }

        private object Convert(decimal value)
        {
            if (value > 0) return Positive;
            else if (value < 0) return Negative;
            else return Zero;
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

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
