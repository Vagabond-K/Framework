using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK.App
{
    /// <summary>
    /// 화면 로드 알림 인터페이스
    /// </summary>
    public interface INotifyLoaded
    {
        /// <summary>
        /// 화면이 로드되었을 때 호출됨.
        /// </summary>
        void OnLoaded();
    }
}
