using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VagabondK
{
    public class CloseWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is Window;
        }

        public void Execute(object parameter)
        {
            (parameter as Window)?.Close();
        }
    }
}
