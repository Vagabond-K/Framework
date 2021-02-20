using AppSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VagabondK;

namespace AppSample.ViewModels
{
    [ServiceDescription]
    public class MainPage : NotifyPropertyChangeObject
    {
        public MainPage(PageContext pageContext)
        {
            this.pageContext = pageContext;
        }

        private readonly PageContext pageContext;

        public SmsSetting SmsSetting { get => Get(() => new SmsSetting()); }

        public ICommand ShowDialogCommand
        {
            get => GetCommand(async () =>
            {
                if (await pageContext.ServiceProvider.ShowDialog<SmsSettingDialog, Views.SmsSettingDialogView>("SMS ettings", out var result) == true)
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
