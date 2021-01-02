using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsSample.ViewModels
{
    class MainViewModel : NotifyPropertyChangeObject
    {
        public double Int32Value { get => Get(0); set => Set(value); }
        public double FloatValue { get => Get(0f); set => Set(value); }
        public double DoubleValue { get => Get(0d); set => Set(value); }

        public bool IsEnabledCommands
        {
            get => Get(true);
            set
            {
                if (Set(value))
                {
                    IncreaseCommand.RaiseCanExecuteChanged();
                    DecreaseCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public IInstantCommand IncreaseCommand
        {
            get => Get(() =>
            {
                Int32Value += 1;
                FloatValue += 0.1;
                DoubleValue += 0.01;
            }, () => IsEnabledCommands);
        }

        public IInstantCommand DecreaseCommand
        {
            get => Get(() =>
            {
                Int32Value -= 1;
                FloatValue -= 0.1;
                DoubleValue -= 0.01;
            }, () => IsEnabledCommands);
        }
    }
}
