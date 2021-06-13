using System;
using System.Collections.Generic;
using System.Text;

namespace VagabondK.App
{
    /// <summary>
    /// 사용자가 클릭하는 메시지 상자 단추를 지정합니다.
    /// </summary>
    public enum MessageBoxResult
    {
        /// <summary>
        /// 메시지 상자가 결과를 반환하지 않습니다.
        /// </summary>
        None = 0,
        /// <summary>
        /// 메시지 상자의 결과 값이 확인입니다.
        /// </summary>
        OK = 1,
        /// <summary>
        /// 메시지 상자의 결과 값이 취소입니다.
        /// </summary>
        Cancel = 2,
        /// <summary>
        /// 메시지 상자의 결과 값이 예입니다.
        /// </summary>
        Yes = 6,
        /// <summary>
        /// 메시지 상자의 결과 값이 아니요입니다.
        /// </summary>
        No = 7
    }
}
