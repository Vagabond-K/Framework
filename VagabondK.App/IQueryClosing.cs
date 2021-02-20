using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK
{
    /// <summary>
    /// 닫히기 전 질의를 위한 인터페이스
    /// </summary>
    public interface IQueryClosing
    {
        /// <summary>
        /// 페이지를 닫기 여부 질의
        /// </summary>
        /// <param name="result">닫히기 전의 페이지 결과</param>
        /// <returns>페이지 닫기 여부</returns>
        Task<bool> QueryClosing(bool result);
    }
}
