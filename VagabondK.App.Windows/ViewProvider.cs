using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VagabondK.App.Windows
{
    /// <summary>
    /// 뷰 공급자
    /// </summary>
    [ServiceDescription(typeof(App.ViewProvider))]
    public class ViewProvider : App.ViewProvider
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        public ViewProvider(IServiceProvider serviceProvider) : base(serviceProvider) { }

        /// <summary>
        /// 해당 뷰 형식이 리로드 가능한 타입인지 여부를 반환합니다.
        /// </summary>
        /// <param name="viewType">뷰 형식</param>
        /// <returns>리로드 가능 여부</returns>
        protected override bool CanReload(Type viewType) => viewType != null && !typeof(Window).IsAssignableFrom(viewType);
    }
}
