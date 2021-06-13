using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VagabondK.Windows
{
    /// <summary>
    /// 윈도우 최소화 커맨드
    /// </summary>
    public class MinimizeWindowCommand : ICommand
    {
        /// <summary>
        /// 명령을 실행해야 하는지 여부에 영향을 주는 변경이 발생할 때 발생합니다.
        /// </summary>
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 명령을 현재 상태에서 실행할 수 있는지를 결정하는 메서드를 정의합니다.
        /// </summary>
        /// <param name="parameter">명령에 사용된 데이터입니다. 명령에서 데이터를 전달할 필요가 없으면 이 개체를 null로 설정할 수 있습니다.</param>
        /// <returns>이 명령을 실행할 수 있으면 true이고, 그러지 않으면 false입니다.</returns>
        public bool CanExecute(object parameter)
        {
            return parameter is Window;
        }

        /// <summary>
        /// 명령이 호출될 때 호출될 메서드를 정의합니다.
        /// </summary>
        /// <param name="parameter">명령에 사용된 데이터입니다. 명령에서 데이터를 전달할 필요가 없으면 이 개체를 null로 설정할 수 있습니다.</param>
        public void Execute(object parameter)
        {
            if (parameter is Window window)
                window.WindowState = WindowState.Minimized;
        }
    }
}
