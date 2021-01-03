using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK.Converters
{
    class Helper
    {
        internal static bool IsBooleanConvertable(object value)
            => value is bool
            || value is sbyte
            || value is short
            || value is int
            || value is long
            || value is float
            || value is double
            || value is byte
            || value is ushort
            || value is uint
            || value is ulong
            || value is string
            || value is decimal;
    }
}
