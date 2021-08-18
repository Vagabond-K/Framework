using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK.App
{
    /// <summary>
    /// 뷰 공급자
    /// </summary>
    public abstract class ViewProvider
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        protected ViewProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 해당 뷰 형식이 리로드 가능한 타입인지 여부를 반환합니다.
        /// </summary>
        /// <param name="viewType">뷰 형식</param>
        /// <returns>리로드 가능 여부</returns>
        protected abstract bool CanReload(Type viewType);

        internal bool CanReloadInternal(Type viewType) => CanReload(viewType);
        internal object GetView(Type viewType) => serviceProvider.GetService(viewType);
    }
}
