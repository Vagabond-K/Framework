namespace System.ComponentModel
{
    /// <summary>
    /// 속성 변경 이벤트 처리 전에 취소 여부 질의가 가능하게 함.
    /// </summary>
    public abstract class QueryPropertyChangingEventArgs : PropertyChangingEventArgs
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="propertyName">속성 명</param>
        protected QueryPropertyChangingEventArgs(string propertyName) : base(propertyName) { }

        /// <summary>
        /// 속성 변경 취소 여부
        /// </summary>
        public bool IsCanceled { get; set; }
    }

    /// <summary>
    /// 속성 변경 이벤트 처리 전에 취소 여부 질의가 가능하게 함.
    /// </summary>
    /// <typeparam name="TProperty">속성의 형식</typeparam>
    public class QueryPropertyChangingEventArgs<TProperty> : QueryPropertyChangingEventArgs
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="newValue">새 속성 값</param>
        /// <param name="propertyName">속성 명</param>
        public QueryPropertyChangingEventArgs(string propertyName, TProperty newValue) : base(propertyName)
        {
            NewValue = newValue;
        }

        /// <summary>
        /// 새 속성 값
        /// </summary>
        public TProperty NewValue { get; }
    }
}
