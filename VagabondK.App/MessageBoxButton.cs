using System;
using System.Collections.Generic;
using System.Text;

namespace VagabondK.App
{
    /// <summary>
    /// 메시지 상자에 표시되는 단추를 지정합니다.
    /// </summary>
    public enum MessageBoxButton
    {
        /// <summary>
        /// 메시지 상자에 확인 단추가 표시됩니다.
        /// </summary>
        OK = 0,
        /// <summary>
        /// 메시지 상자에 확인 및 취소 단추가 표시됩니다.
        /// </summary>
        OKCancel = 1,
        /// <summary>
        /// 메시지 상자에 예, 아니요 및 취소 단추가 표시됩니다.
        /// </summary>
        YesNoCancel = 3,
        /// <summary>
        /// 메시지 상자에 예 및 아니요 단추가 표시됩니다.
        /// </summary>
        YesNo = 4
    }
}
