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

namespace AppSample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : ThemeWindow
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
            await shell.OpenPage<ViewModels.MainPage, Views.MainPageView>("Main Page");
        }

        //private const int WM_SYSCOMMAND = 0x112;
        //uint TPM_LEFTALIGN = 0x0000;
        //uint TPM_RETURNCMD = 0x0100;
        //const UInt32 MF_ENABLED = 0x00000000;
        //const UInt32 MF_GRAYED = 0x00000001;
        //internal const UInt32 SC_MAXIMIZE = 0xF030;
        //internal const UInt32 SC_RESTORE = 0xF120;

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        //[DllImport("user32.dll")]
        //static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags,
        //  int x, int y, IntPtr hwnd, IntPtr lptpm);

        //[DllImport("user32.dll")]
        //public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll")]
        //static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem,
        //   uint uEnable);

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    WindowInteropHelper helper = new WindowInteropHelper(this);
        //    IntPtr callingWindow = helper.Handle;
        //    IntPtr wMenu = GetSystemMenu(callingWindow, false);
        //    // Display the menu
        //    if (this.WindowState == System.Windows.WindowState.Maximized)
        //    {
        //        EnableMenuItem(wMenu, SC_MAXIMIZE, MF_GRAYED);
        //    }
        //    else
        //    {
        //        EnableMenuItem(wMenu, SC_MAXIMIZE, MF_ENABLED);
        //    }

        //    int command = TrackPopupMenuEx(wMenu, TPM_LEFTALIGN | TPM_RETURNCMD, 100, 100, callingWindow, IntPtr.Zero);
        //    if (command == 0)
        //        return;

        //    PostMessage(callingWindow, WM_SYSCOMMAND, new IntPtr(command), IntPtr.Zero);
        //}
    }
}
