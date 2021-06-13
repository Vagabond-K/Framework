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
    /// 수치값에 대한 제한 적용 변환
    /// </summary>
    public class NumericLimitConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// 최대값
        /// </summary>
        public decimal Maximum { get; set; }

        /// <summary>
        /// 최소값
        /// </summary>
        public decimal Minimum { get; set; }

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

            var typeCode = System.Convert.GetTypeCode(value);

            if (typeCode == TypeCode.SByte
                || typeCode == TypeCode.Int16
                || typeCode == TypeCode.Int32
                || typeCode == TypeCode.Int64
                || typeCode == TypeCode.Single
                || typeCode == TypeCode.Double
                || typeCode == TypeCode.Decimal
                || typeCode == TypeCode.Byte
                || typeCode == TypeCode.UInt16
                || typeCode == TypeCode.UInt32
                || typeCode == TypeCode.UInt64)
            {
                decimal result = value.To<decimal>();

                if (result > Maximum) result = Maximum;
                if (result < Minimum) result = Minimum;

                return System.Convert.ChangeType(result, typeCode);
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
