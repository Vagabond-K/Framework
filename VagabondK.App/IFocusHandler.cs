using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK
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
}
