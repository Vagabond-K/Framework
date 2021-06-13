using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK.App
{
    /// <summary>
    /// 메시지 상자 또는 메시지 알림에 표시되는 아이콘을 지정합니다.
    /// </summary>
    public enum MessageImage
    {
        /// <summary>
        /// 아이콘이 표시되지 않습니다.
        /// </summary>
        None = 0,
        /// <summary>
        /// 메시지 상자에는 빨간색 배경의 원 안에 흰색 X가 포함된 기호가 있습니다.
        /// </summary>
        Hand = 16,
        /// <summary>
        /// 메시지 상자에는 빨간색 배경의 원 안에 흰색 X가 포함된 기호가 있습니다.
        /// </summary>
        Stop = 16,
        /// <summary>
        /// 메시지 상자에는 빨간색 배경의 원 안에 흰색 X가 포함된 기호가 있습니다.
        /// </summary>
        Error = 16,
        /// <summary>
        /// 메시지 상자에는 원 안에 물음표가 포함된 기호가 있습니다.
        /// </summary>
        Question = 32,
        /// <summary>
        /// 메시지 상자에는 노란색 배경의 삼각형 안에 느낌표가 포함된 기호가 있습니다.
        /// </summary>
        Exclamation = 48,
        /// <summary>
        /// 메시지 상자에는 노란색 배경의 삼각형 안에 느낌표가 포함된 기호가 있습니다.
        /// </summary>
        Warning = 48,
        /// <summary>
        /// 메시지 상자에는 원 안에 소문자 i가 포함된 기호가 있습니다.
        /// </summary>
        Asterisk = 64,
        /// <summary>
        /// 메시지 상자에는 원 안에 소문자 i가 포함된 기호가 있습니다.
        /// </summary>
        Information = 64
    }
}
