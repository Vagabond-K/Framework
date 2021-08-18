using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VagabondK;
using VagabondK.App;
using VagabondK.App.Windows;

namespace AppSample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : VagabondK.Windows.ThemeWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

            ServiceCollection services = new ServiceCollection();
            services.AddServices(typeof(Dialog).Assembly);
            services.AddServices(typeof(MainWindow).Assembly);
            shell = new SimpleShell(services);
            DataContext = shell;
        }

        private readonly Shell shell;

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await shell.OpenPage<ViewModels.MainPage>("Main Page");
        }
    }
}
