namespace System
{
    /// <summary>
    /// 데이터 변환 확장 메서드 모음
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// 지정된 object를 특정 형식으로 변환하고, object가 null이거나 변환 중 예외 발생 시 기본 값을 반환.
        /// </summary>
        /// <typeparam name="T">변환하고자 하는 형식</typeparam>
        /// <param name="value">입력 값</param>
        /// <param name="defaultValue">기본 반환 값</param>
        /// <returns>변환 결과</returns>
        public static T To<T>(this object value, T defaultValue = default(T))
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
    }
}
