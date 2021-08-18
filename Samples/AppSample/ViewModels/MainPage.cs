using AppSample.Models;
using System;
using System.Windows.Input;
using VagabondK.App;

namespace AppSample.ViewModels
{
    [ViewModel(DefaultViewType = typeof(Views.MainPageView))]
    public class MainPage : NotifyPropertyChangeObject, INotifyLoaded
    {
        public MainPage(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private readonly IServiceProvider serviceProvider;

        public async void OnLoaded()
        {
            await serviceProvider.ShowMessageBox("메인 페이지 로드 완료!", "알림", MessageBoxButton.OK, MessageImage.Information);
        }

        public SmsSetting SmsSetting { get => Get(() => new SmsSetting()); }

        public ICommand ShowDialogCommand
        {
            get => GetCommand(async () =>
            {
                if (await serviceProvider.ShowDialog<SmsSettingDialog>("SMS ettings", out var result) == true)
                {
                    SmsSetting.ServiceID = result.SmsSetting.ServiceID;
                    SmsSetting.AccessKeyID = result.SmsSetting.AccessKeyID;
                    SmsSetting.SecretKey = result.SmsSetting.SecretKey;
                    SmsSetting.SenderPhoneNumber = result.SmsSetting.SenderPhoneNumber;
                }
            });
        }
    }
}
