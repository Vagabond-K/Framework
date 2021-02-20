using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK
{
    /// <summary>
    /// 메시지 상자 서비스
    /// </summary>
    [ServiceDescription]
    public interface IMessageBox
    {
        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <param name="icon">아이콘</param>
        /// <param name="defaultResult">기본 선택 메시지 상자 결과</param>
        /// <returns>메시지 상자 결과 반환 태스크</returns>
        Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button, MessageImage icon, MessageBoxResult defaultResult);
    }

    /// <summary>
    /// 메시지 상자 서비스 확장 메서드 모음
    /// </summary>
    public static class IMessageBoxExtensions
    {
        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBox">메시지 상자 서비스</param>
        /// <param name="messageBoxText">메시지</param>
        /// <returns>메시지 상자 결과 반환 태스크</returns>
        public static Task<MessageBoxResult> Show(this IMessageBox messageBox, string messageBoxText) => messageBox.Show(messageBoxText, string.Empty);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBox">메시지 상자 서비스</param>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <returns>메시지 상자 결과 반환 태스크</returns>
        public static Task<MessageBoxResult> Show(this IMessageBox messageBox, string messageBoxText, string caption) => messageBox.Show(messageBoxText, caption, MessageBoxButton.OK);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBox">메시지 상자 서비스</param>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <returns>메시지 상자 결과 반환 태스크</returns>
        public static Task<MessageBoxResult> Show(this IMessageBox messageBox, string messageBoxText, MessageBoxButton button) => messageBox.Show(messageBoxText, string.Empty, button);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBox">메시지 상자 서비스</param>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="icon">아이콘</param>
        /// <returns>메시지 상자 결과 반환 태스크</returns>
        public static Task<MessageBoxResult> Show(this IMessageBox messageBox, string messageBoxText, MessageImage icon) => messageBox.Show(messageBoxText, string.Empty, MessageBoxButton.OK, icon);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBox">메시지 상자 서비스</param>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <returns>메시지 상자 결과 반환 태스크</returns>
        public static Task<MessageBoxResult> Show(this IMessageBox messageBox, string messageBoxText, string caption, MessageBoxButton button) => messageBox.Show(messageBoxText, caption, button, MessageImage.None);

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBox">메시지 상자 서비스</param>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <param name="icon">아이콘</param>
        /// <returns>메시지 상자 결과 반환 태스크</returns>
        public static Task<MessageBoxResult> Show(this IMessageBox messageBox, string messageBoxText, string caption, MessageBoxButton button, MessageImage icon) => messageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.None);
    }
}
