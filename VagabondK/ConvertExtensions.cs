namespace System
{
    /// <summary>
    /// 데이터 변환 확장 메서드 모음
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// value를 특정 형식으로 변환하고, value가 null이거나 변환 중 예외 발생 시 기본 값을 반환.
        /// </summary>
        /// <typeparam name="T">변환하고자 하는 형식</typeparam>
        /// <param name="value">입력 값</param>
        /// <param name="defaultValue">기본 반환 값</param>
        /// <returns>변환 결과</returns>
        public static T To<T>(this object value, T defaultValue = default) where T : struct
        {
            if (value == null)
                return defaultValue;
            
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// value를 특정 형식에 대한 Nullable로 변환하고, value가 null이거나 변환 중 예외 발생 시 null을 반환.
        /// </summary>
        /// <typeparam name="T">변환하고자 하는 Nullable의 형식 매개변수</typeparam>
        /// <param name="value">입력 값</param>
        /// <returns>변환 결과</returns>
        public static T? ToNullable<T>(this object value) where T : struct
        {
            if (value == null)
                return null;

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// value를 특정 형식으로 변환 시도
        /// </summary>
        /// <typeparam name="T">변환하고자 하는 형식</typeparam>
        /// <param name="value">입력 값</param>
        /// <param name="result">변환 결과</param>
        /// <returns>변환 성공 여부</returns>
        public static bool TryConvert<T>(this object value, out T result) where T : struct
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            try
            {
                result = (T)Convert.ChangeType(value, typeof(T));
                return true;
            }
            catch
            {
                result = default;
            }
            return false;
        }
    }
}
