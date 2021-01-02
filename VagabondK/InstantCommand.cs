using System.Reflection;
using System.Threading;
using System.Windows.Input;

namespace System
{
    /// <summary>
    /// 즉석에서 정의해서 사용하는 커맨드 인터페이스
    /// </summary>
    public interface IInstantCommand : ICommand
    {
        /// <summary>
        /// CanExecuteChanged 이벤트를 발생시킴.
        /// </summary>
        void RaiseCanExecuteChanged();
    }

    /// <summary>
    /// 즉석에서 정의해서 사용하는 커맨드의 기본 클래스
    /// </summary>
    public abstract class InstantCommandBase : IInstantCommand
    {
        /// <summary>
        /// 생성자
        /// </summary>
        protected InstantCommandBase()
        {
            synchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public virtual event EventHandler CanExecuteChanged;

        private readonly SynchronizationContext synchronizationContext;

        /// <summary>
        /// CanExecuteChanged 이벤트를 발생시킴.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged != null)
            {
                if (synchronizationContext != null && synchronizationContext != SynchronizationContext.Current)
                    synchronizationContext.Post((o) => canExecuteChanged.Invoke(this, EventArgs.Empty), null);
                else
                    canExecuteChanged.Invoke(this, EventArgs.Empty);
            }
        }

        void IInstantCommand.RaiseCanExecuteChanged() => OnCanExecuteChanged();

        void ICommand.Execute(object parameter) => Execute(parameter);
        bool ICommand.CanExecute(object parameter) => CanExecute(parameter);

        /// <summary>
        /// 커맨드 실행
        /// </summary>
        /// <param name="parameter">커맨드 파라미터</param>
        protected abstract void Execute(object parameter);

        /// <summary>
        /// 커맨드 실행 가능 여부를 반환
        /// </summary>
        /// <param name="parameter">커맨드 파라미터</param>
        /// <returns>커맨드 실행 가능 여부</returns>
        protected abstract bool CanExecute(object parameter);
    }

    /// <summary>
    /// 즉석에서 정의해서 사용하는 커맨드
    /// </summary>
    public class InstantCommand : InstantCommandBase
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="executeAction">커맨드 실행 Action</param>
        public InstantCommand(Action executeAction) : this(executeAction, () => true) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="executeAction">커맨드 실행 Action</param>
        /// <param name="canExecuteFunc">커맨드 실행 가능 여부 Func</param>
        public InstantCommand(Action executeAction, Func<bool> canExecuteFunc) : base()
        {
            this.executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            this.canExecuteFunc = canExecuteFunc ?? throw new ArgumentNullException(nameof(canExecuteFunc));
        }

        private readonly Action executeAction;
        private readonly Func<bool> canExecuteFunc;

        /// <summary>
        /// 커맨드 실행
        /// </summary>
        public void Execute() => executeAction();

        /// <summary>
        /// 커맨드 실행
        /// </summary>
        /// <param name="parameter">커맨드 파라미터</param>
        protected override void Execute(object parameter) => Execute();

        /// <summary>
        /// 커맨드 실행 가능 여부를 반환
        /// </summary>
        /// <returns>커맨드 실행 가능 여부</returns>
        public bool CanExecute() => canExecuteFunc();

        /// <summary>
        /// 커맨드 실행 가능 여부를 반환
        /// </summary>
        /// <param name="parameter">커맨드 파라미터</param>
        /// <returns>커맨드 실행 가능 여부</returns>
        protected override bool CanExecute(object parameter) => CanExecute();
    }

    /// <summary>
    /// 즉석에서 정의해서 사용하는 파라미터가 포함된 커맨드
    /// </summary>
    /// <typeparam name="TParameter">파라미터 형식</typeparam>
    public class InstantCommand<TParameter> : InstantCommandBase
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="executeAction">커맨드 실행 Action</param>
        public InstantCommand(Action<TParameter> executeAction) : this(executeAction, (o) => true) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="executeAction">커맨드 실행 Action</param>
        /// <param name="canExecuteFunc">커맨드 실행 가능 여부 Func</param>
        public InstantCommand(Action<TParameter> executeAction, Func<TParameter, bool> canExecuteFunc) : base()
        {
            this.executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            this.canExecuteFunc = canExecuteFunc ?? throw new ArgumentNullException(nameof(canExecuteFunc));

            TypeInfo genericType = typeof(TParameter).GetTypeInfo();

            if (genericType.IsValueType
                && (!genericType.IsGenericType || (!typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(genericType.GetGenericTypeDefinition().GetTypeInfo()))))
                throw new InvalidCastException();
        }

        private readonly Action<TParameter> executeAction;
        private readonly Func<TParameter, bool> canExecuteFunc;

        /// <summary>
        /// 커맨드 실행
        /// </summary>
        /// <param name="parameter">커맨드 파라미터</param>
        public void Execute(TParameter parameter) => executeAction(parameter);

        /// <summary>
        /// 커맨드 실행
        /// </summary>
        /// <param name="parameter">커맨드 파라미터</param>
        protected override void Execute(object parameter) => Execute((TParameter)parameter);

        /// <summary>
        /// 커맨드 실행 가능 여부를 반환
        /// </summary>
        /// <param name="parameter">커맨드 파라미터</param>
        /// <returns>커맨드 실행 가능 여부</returns>
        public bool CanExecute(TParameter parameter) => canExecuteFunc(parameter);

        /// <summary>
        /// 커맨드 실행 가능 여부를 반환
        /// </summary>
        /// <param name="parameter">커맨드 파라미터</param>
        /// <returns>커맨드 실행 가능 여부</returns>
        protected override bool CanExecute(object parameter) => CanExecute((TParameter)parameter);
    }
}
