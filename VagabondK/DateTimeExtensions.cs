namespace System
{
    /// <summary>
    /// DateTime 확장 메서드 모음
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 입력 DateTime 값의 년도 중 첫 번째 날짜를 반환
        /// </summary>
        /// <param name="dateTime">입력 값</param>
        /// <returns>입력 DateTime 값의 년도 중 첫 번째 날짜</returns>
        public static DateTime FirstDayInYear(this DateTime dateTime) => new DateTime(dateTime.Year, 1, 1);

        /// <summary>
        /// 입력 DateTime 값의 년월 중 첫 번째 날짜를 반환
        /// </summary>
        /// <param name="dateTime">입력 값</param>
        /// <returns>입력 DateTime 값의 년월 중 첫 번째 날짜</returns>
        public static DateTime FirstDayInMonth(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, 1);

        /// <summary>
        /// 입력 DateTime에서 분, 초, 밀리초를 자르고 반환
        /// </summary>
        /// <param name="dateTime">입력 값</param>
        /// <returns>입력 DateTime에서 분, 초, 밀리초를 자른 값</returns>
        public static DateTime CutoutAfterHour(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0);

        /// <summary>
        /// 입력 DateTime에서 초, 밀리초를 자르고 반환
        /// </summary>
        /// <param name="dateTime">입력 값</param>
        /// <returns>입력 DateTime에서 초, 밀리초를 자른 값</returns>
        public static DateTime CutoutAfterMinute(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);

        /// <summary>
        /// 입력 DateTime에서 밀리초를 자르고 반환
        /// </summary>
        /// <param name="dateTime">입력 값</param>
        /// <returns>입력 DateTime에서 밀리초를 자른 값</returns>
        public static DateTime CutoutAfterSecond(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
    }
}