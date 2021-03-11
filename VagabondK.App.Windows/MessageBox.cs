using System;
using System.Threading.Tasks;

namespace VagabondK
{
    /// <summary>
    /// 메시지 상자 관련 기능
    /// </summary>
    [ServiceDescription(typeof(IMessageBox))]
    public class MessageBox : IMessageBox
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="pageContext">페이지 컨텍스트</param>
        public MessageBox(PageContext pageContext)
        {
            this.pageContext = pageContext;
        }

        private readonly PageContext pageContext;

        /// <summary>
        /// 메시지 상자 표시
        /// </summary>
        /// <param name="messageBoxText">메시지</param>
        /// <param name="caption">제목</param>
        /// <param name="button">메시지 상자에 표시되는 단추</param>
        /// <param name="icon">아이콘</param>
        /// <param name="defaultResult">기본 선택 메시지 상자 결과</param>
        /// <returns>메시지 상자 결과 반환 태스크</returns>
        public Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button, MessageImage icon, MessageBoxResult defaultResult)
            => Task.FromResult((MessageBoxResult)(int)
                (pageContext?.View is System.Windows.DependencyObject dependencyObject ?
                ThemeMessageBox.Show(System.Windows.Window.GetWindow(dependencyObject), messageBoxText, caption,
                (System.Windows.MessageBoxButton)(int)button,
                (System.Windows.MessageBoxImage)(int)icon,
                (System.Windows.MessageBoxResult)(int)defaultResult) :
                ThemeMessageBox.Show(messageBoxText, caption,
                (System.Windows.MessageBoxButton)(int)button,
                (System.Windows.MessageBoxImage)(int)icon,
                (System.Windows.MessageBoxResult)(int)defaultResult)));
    }
}
