using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
    /// <summary>
    /// 이미 다른 클래스를 상속 받아서 NotifyPropertyChangeObject를 상속받지 못할 때 내부 속성 저장소로 사용하는 클래스
    /// </summary>
    public sealed class PropertyValues : NotifyPropertyChangeObject
    {
        private PropertyValues(object eventSource, PropertyChangingEventHandler propertyChangingEventHandler, PropertyChangedEventHandler propertyChangedEventHandler) : base(eventSource)
        {
            PropertyChanging += propertyChangingEventHandler;
            PropertyChanged += propertyChangedEventHandler;
        }

        /// <summary>
        /// PropertyValues 객체 생성
        /// </summary>
        /// <typeparam name="TEventSource">PropertyChanged 이벤트를 포함하는 객체 형식</typeparam>
        /// <param name="eventSource">PropertyChanged 이벤트를 포함하는 객체</param>
        /// <param name="propertyChangedEventHandler">PropertyChanged 이벤트 처리기</param>
        /// <returns>PropertyValues 객체</returns>
        public static PropertyValues Create<TEventSource>(TEventSource eventSource, PropertyChangedEventHandler propertyChangedEventHandler) where TEventSource : class, INotifyPropertyChanged
            => new PropertyValues(eventSource, null, propertyChangedEventHandler);

        /// <summary>
        /// PropertyValues 객체 생성
        /// </summary>
        /// <typeparam name="TEventSource">PropertyChanging 이벤트를 포함하는 객체 형식</typeparam>
        /// <param name="eventSource">PropertyChanging 이벤트를 포함하는 객체</param>
        /// <param name="propertyChangingEventHandler">PropertyChanging 이벤트 처리기</param>
        /// <returns>PropertyValues 객체</returns>
        public static PropertyValues Create<TEventSource>(TEventSource eventSource, PropertyChangingEventHandler propertyChangingEventHandler) where TEventSource : class, INotifyPropertyChanging
            => new PropertyValues(eventSource, propertyChangingEventHandler, null);

        /// <summary>
        /// PropertyValues 객체 생성
        /// </summary>
        /// <typeparam name="TEventSource">PropertyChanging, PropertyChanged 이벤트를 포함하는 객체 형식</typeparam>
        /// <param name="eventSource">PropertyChanging, PropertyChanged 이벤트를 포함하는 객체</param>
        /// <param name="propertyChangingEventHandler">PropertyChanging 이벤트 처리기</param>
        /// <param name="propertyChangedEventHandler">PropertyChanged 이벤트 처리기</param>
        /// <returns>PropertyValues 객체</returns>
        public static PropertyValues Create<TEventSource>(TEventSource eventSource, PropertyChangingEventHandler propertyChangingEventHandler, PropertyChangedEventHandler propertyChangedEventHandler) where TEventSource : class, INotifyPropertyChanging, INotifyPropertyChanged
            => new PropertyValues(eventSource, propertyChangingEventHandler, propertyChangedEventHandler);

        /// <summary>
        /// 속성 값 가져오기
        /// </summary>
        /// <typeparam name="TProperty">속성 형식</typeparam>
        /// <param name="factory">속성 초기화 동작</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>속성 값</returns>
        public new TProperty Get<TProperty>(Func<TProperty> factory, [CallerMemberName]string propertyName = null)
            => base.Get(factory, propertyName);

        /// <summary>
        /// 속성 값 가져오기
        /// </summary>
        /// <typeparam name="TProperty">속성 형식</typeparam>
        /// <param name="defaultValue">초기 기본 값</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>속성 값</returns>
        public new TProperty Get<TProperty>(TProperty defaultValue = default, [CallerMemberName]string propertyName = null)
            => base.Get(defaultValue, propertyName);

        /// <summary>
        /// 속성 값 설정하기
        /// </summary>
        /// <typeparam name="TProperty">속성 형식</typeparam>
        /// <param name="value">설정할 값</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>설정 여부</returns>
        public new bool Set<TProperty>(TProperty value, [CallerMemberName]string propertyName = null)
            => Set(value, propertyName);
    }
}
