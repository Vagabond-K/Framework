using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace WindowsSample
{
    public partial class App : Application
    {
        public App()
        {
            VagabondK.ThemeManager.AppThemeName = "Dark";
        }
    }
}
