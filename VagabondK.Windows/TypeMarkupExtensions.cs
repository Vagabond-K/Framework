using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace VagabondK
{
    /// <summary>
    /// Boolean 타입 태그 확장
    /// </summary>
    public class BooleanExtension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public BooleanExtension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<bool>();
        }
    }

    /// <summary>
    /// Double 타입 태그 확장
    /// </summary>
    public class DoubleExtension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public DoubleExtension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<double>();
        }
    }

    /// <summary>
    /// Single 타입 태그 확장
    /// </summary>
    public class SingleExtension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public SingleExtension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<float>();
        }
    }

    /// <summary>
    /// SByte 타입 태그 확장
    /// </summary>
    public class SByteExtension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public SByteExtension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<sbyte>();
        }
    }

    /// <summary>
    /// Byte 타입 태그 확장
    /// </summary>
    public class ByteExtension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public ByteExtension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<byte>();
        }
    }

    /// <summary>
    /// Int16 타입 태그 확장
    /// </summary>
    public class Int16Extension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public Int16Extension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<short>();
        }
    }


    /// <summary>
    /// UInt16 타입 태그 확장
    /// </summary>
    public class UInt16Extension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public UInt16Extension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<ushort>();
        }
    }


    /// <summary>
    /// Int32 타입 태그 확장
    /// </summary>
    public class Int32Extension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public Int32Extension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<int>();
        }
    }


    /// <summary>
    /// UInt32 타입 태그 확장
    /// </summary>
    public class UInt32Extension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public UInt32Extension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<uint>();
        }
    }

    /// <summary>
    /// Int64 타입 태그 확장
    /// </summary>
    public class Int64Extension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public Int64Extension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<long>();
        }
    }


    /// <summary>
    /// UInt64 타입 태그 확장
    /// </summary>
    public class UInt64Extension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public UInt64Extension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<ulong>();
        }
    }

    /// <summary>
    /// Decimal 타입 태그 확장
    /// </summary>
    public class DecimalExtension : MarkupExtension
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="value">입력값</param>
        public DecimalExtension(object value)
        {
            this.value = value;
        }

        private readonly object value = null;

        /// <summary>
        /// 태그 확장의 대상 속성 값으로 제공된 개체를 반환합니다.
        /// </summary>
        /// <param name="serviceProvider">태그 확장명 서비스를 제공할 수 있는 서비스 공급자 도우미입니다.</param>
        /// <returns>확장이 적용되는 속성에 설정할 개체 값입니다.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return value.To<decimal>();
        }
    }
}
