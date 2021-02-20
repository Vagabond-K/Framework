using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VagabondK
{
    public class MinimizeWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is Window;
        }

        public void Execute(object parameter)
        {
            if (parameter is Window window)
                window.WindowState = WindowState.Minimized;
        }
    }
}
