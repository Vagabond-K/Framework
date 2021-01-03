using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace VagabondK.Converters
{
    /// <summary>
    /// Double 타입으로 변환
    /// </summary>
    public class ToDoubleConverter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<double>());
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

    /// <summary>
    /// Single 타입으로 변환
    /// </summary>
    public class ToSingleConverter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<float>());
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

    /// <summary>
    /// Int64 타입으로 변환
    /// </summary>
    public class ToInt64Converter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<long>());
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

    /// <summary>
    /// UInt64 타입으로 변환
    /// </summary>
    public class ToUInt64Converter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<ulong>());
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

    /// <summary>
    /// Int32 타입으로 변환
    /// </summary>
    public class ToInt32Converter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<int>());
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

    /// <summary>
    /// UInt32 타입으로 변환
    /// </summary>
    public class ToUInt32Converter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<uint>());
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

    /// <summary>
    /// Int16 타입으로 변환
    /// </summary>
    public class ToInt16Converter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<short>());
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

    /// <summary>
    /// UInt16 타입으로 변환
    /// </summary>
    public class ToUInt16Converter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<ushort>());
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


    /// <summary>
    /// Byte 타입으로 변환
    /// </summary>
    public class ToByteConverter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<byte>());
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


    /// <summary>
    /// SByte 타입으로 변환
    /// </summary>
    public class ToSByteConverter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<sbyte>());
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

    /// <summary>
    /// Decimal 타입으로 변환
    /// </summary>
    public class ToDecimalConverter : MarkupExtension, IValueConverter
    {
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
            return value.To(parameter.To<decimal>());
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
