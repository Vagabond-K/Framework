using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK.App
{
    /// <summary>
    /// 포커스 핸들러
    /// </summary>
    public interface IFocusHandler
    {
        /// <summary>
        /// 포커스 설정
        /// </summary>
        /// <param name="name">포커스를 설정할 컨트롤 또는 속성 이름</param>
        /// <returns>포커스 설정 결과</returns>
        bool Focus(string name);
    }

    /// <summary>
    /// 포커스 핸들러 서비스 확장 메서드 모음
    /// </summary>
    public static class FocusHandlerExtensions
    {
        /// <summary>
        /// 포커스 설정
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="name">포커스를 설정할 컨트롤 또는 속성 이름</param>
        /// <returns>포커스 설정 결과</returns>
        public static bool Focus(this IServiceProvider serviceProvider, string name) => serviceProvider.GetRequiredService<IFocusHandler>().Focus(name);
    }
}
