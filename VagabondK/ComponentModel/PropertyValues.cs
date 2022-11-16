using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
    /// <summary>
    /// 이미 다른 클래스를 상속 받아서 NotifyPropertyChangeObject를 상속받지 못할 때 내부 속성 저장소로 사용하는 클래스
    /// </summary>
    public sealed class PropertyValues : NotifyPropertyChangeObject
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="eventSource">이벤트 발생 소스</param>
        public PropertyValues(object eventSource) : base(eventSource) { }

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
        /// 즉석 커맨드 가져오기
        /// </summary>
        /// <param name="executeAction">커맨드 실행 Action</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>커맨드</returns>
        public new InstantCommand GetCommand(Action executeAction, [CallerMemberName] string propertyName = null)
            => Get(() => new InstantCommand(executeAction), propertyName);

        /// <summary>
        /// 즉석 커맨드 가져오기
        /// </summary>
        /// <param name="executeAction">커맨드 실행 Action</param>
        /// <param name="canExecuteFunc">커맨드 실행 가능 여부 Func</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>커맨드</returns>
        public new InstantCommand GetCommand(Action executeAction, Func<bool> canExecuteFunc, [CallerMemberName] string propertyName = null)
            => Get(() => new InstantCommand(executeAction, canExecuteFunc), propertyName);

        /// <summary>
        /// 파라미터를 포함한 즉석 커맨드 가져오기
        /// </summary>
        /// <typeparam name="TParameter">파라미터 형식</typeparam>
        /// <param name="executeAction">커맨드 실행 Action</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>커맨드</returns>
        public new InstantCommand<TParameter> GetCommand<TParameter>(Action<TParameter> executeAction, [CallerMemberName] string propertyName = null)
            => Get(() => new InstantCommand<TParameter>(executeAction), propertyName);

        /// <summary>
        /// 파라미터를 포함한 즉석 커맨드 가져오기
        /// </summary>
        /// <typeparam name="TParameter">파라미터 형식</typeparam>
        /// <param name="executeAction">커맨드 실행 Action</param>
        /// <param name="canExecuteFunc">커맨드 실행 가능 여부 Func</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>커맨드</returns>
        public new InstantCommand<TParameter> GetCommand<TParameter>(Action<TParameter> executeAction, Func<TParameter, bool> canExecuteFunc, [CallerMemberName] string propertyName = null)
            => Get(() => new InstantCommand<TParameter>(executeAction, canExecuteFunc), propertyName);

        /// <summary>
        /// 속성 값 설정하기
        /// </summary>
        /// <typeparam name="TProperty">속성 형식</typeparam>
        /// <param name="value">설정할 값</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>설정 여부</returns>
        public new bool Set<TProperty>(TProperty value, [CallerMemberName]string propertyName = null)
            => base.Set(value, propertyName);

        /// <summary>
        /// 속성 값 제거, 제거되면 다시 Get으로 속성 값을 가져올 때 속성 초기화 동작 결과나 기본 값을 반환함.
        /// </summary>
        /// <param name="propertyName">속성 명</param>
        /// <returns>제거 여부</returns>
        public new bool ClearProperty(string propertyName)
            => base.ClearProperty(propertyName);
    }
}
