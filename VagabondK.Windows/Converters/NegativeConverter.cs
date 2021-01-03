using System;
using System.Collections.Generic;
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
    /// 양수/음수 반전 변환
    /// </summary>
    public class NegativeConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// 값을 변환합니다.
        /// </summary>
        /// <param name="value">바인딩 소스에서 생성한 값입니다.</param>
        /// <param name="targetType">바인딩 대상 속성의 형식입니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>변환된 값입니다.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ToNegative(value);

        /// <summary>
        /// 값을 변환합니다.
        /// </summary>
        /// <param name="value">바인딩 대상에서 생성한 값입니다.</param>
        /// <param name="targetType">변환할 대상 형식입니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>변환된 값입니다.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ToNegative(value);

        private object ToNegative(object value)
        {
            if (value == null || value == DependencyProperty.UnsetValue || value == Binding.DoNothing) return value;
            if (value == DBNull.Value) return null;

            var typeCode = System.Convert.GetTypeCode(value);
            switch (typeCode)
            {
                case TypeCode.SByte:
                    return -value.To<sbyte>();
                case TypeCode.Int16:
                    return -value.To<short>();
                case TypeCode.Int32:
                    return -value.To<int>();
                case TypeCode.Int64:
                    return -value.To<long>();
                case TypeCode.Single:
                    return -value.To<float>();
                case TypeCode.Double:
                    return -value.To<double>();
                case TypeCode.Decimal:
                    return -value.To<decimal>();
                case TypeCode.Byte:
                    return System.Convert.ChangeType(-value.To<sbyte>(), typeCode - 1);
                case TypeCode.UInt16:
                    return System.Convert.ChangeType(-value.To<short>(), typeCode - 1);
                case TypeCode.UInt32:
                    return System.Convert.ChangeType(-value.To<int>(), typeCode - 1);
                case TypeCode.UInt64:
                    return System.Convert.ChangeType(-value.To<long>(), typeCode - 1);
                case TypeCode.DateTime:
                case TypeCode.Empty:
                case TypeCode.Object:
                case TypeCode.DBNull:
                case TypeCode.Boolean:
                case TypeCode.Char:
                case TypeCode.String:
                    return value;
            }

            return value;
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
