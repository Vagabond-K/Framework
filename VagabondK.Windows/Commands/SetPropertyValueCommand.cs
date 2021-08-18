using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace VagabondK.Windows.Commands
{
    /// <summary>
    /// 특정 속성에 커맨드 파라미터로 값을 설정하는 커맨드입니다.
    /// </summary>
    public class SetPropertyValueCommand : DependencyObject, ICommand
    {
        /// <summary>
        /// 값을 설정할 속성을 바인딩 합니다.
        /// </summary>
        public object Target
        {
            get { return (object)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        /// <summary>
        /// VagabondK.Windows.Commands.SetPropertyValueCommand.Target 종속성 속성을 식별합니다.
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(object), typeof(SetPropertyValueCommand), new PropertyMetadata(null));

        /// <summary>
        /// 명령을 실행해야 하는지 여부에 영향을 주는 변경이 발생할 때 발생합니다.
        /// </summary>
        public event EventHandler CanExecuteChanged { add { } remove { } }

        /// <summary>
        /// 명령을 현재 상태에서 실행할 수 있는지를 결정하는 메서드를 정의합니다.
        /// </summary>
        /// <param name="parameter">명령에 사용된 데이터입니다. 명령에서 데이터를 전달할 필요가 없으면 이 개체를 null로 설정할 수 있습니다.</param>
        /// <returns>이 명령을 실행할 수 있으면 true이고, 그러지 않으면 false입니다.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// 명령이 호출될 때 호출될 메서드를 정의합니다.
        /// </summary>
        /// <param name="parameter">명령에 사용된 데이터입니다. 명령에서 데이터를 전달할 필요가 없으면 이 개체를 null로 설정할 수 있습니다.</param>
        public void Execute(object parameter)
        {
            Target = parameter;
        }
    }
}
