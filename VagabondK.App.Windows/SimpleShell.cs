using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK
{
    /// <summary>
    /// 단순 쉘
    /// </summary>
    public class SimpleShell : Shell
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="services">추가 서비스 목록</param>
        public SimpleShell(IServiceCollection services) : base(services)
        {
            services?.AddSingleton(this);
        }

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <param name="viewType">페이지 뷰 형식</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public override Task<PageContext> OpenPage(Type viewModelType, Type viewType, string title)
        {
            var pageContext = ShellServiceProvider.CreatePageContext(viewModelType, viewType, title);

            var oldPageContext = SelectedPageContext;

            SelectedPageContext = pageContext;
            (pageContext.ViewModel as INotifyLoaded)?.OnLoaded();

            oldPageContext?.Dispose();
            return Task.FromResult(pageContext);
        }
    }

    /// <summary>
    /// 단순 쉘
    /// </summary>
    /// <typeparam name="TPageData">페이지 데이터 형식</typeparam>
    public class SimpleShell<TPageData> : Shell<TPageData> where TPageData : IPageData
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="services">추가 서비스 목록</param>
        public SimpleShell(IServiceCollection services) : base(services)
        {
            services?.AddSingleton(this);
        }

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <param name="viewType">페이지 뷰 형식</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public override Task<PageContext> OpenPage(Type viewModelType, Type viewType, string title)
            => Task.FromResult(SetSelectedPageContext(ShellServiceProvider.CreatePageContext(viewModelType, viewType, title)));

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="pageData">페이지 데이터</param>
        /// <returns>페이지 컨텍스트</returns>
        public override Task<PageContext<TPageData>> OpenPage(TPageData pageData)
            => Task.FromResult(SetSelectedPageContext(ShellServiceProvider.CreatePageContext(pageData)));

        private TPageContext SetSelectedPageContext<TPageContext>(TPageContext pageContext) where TPageContext : PageContext
        {
            var oldPageContext = SelectedPageContext;

            SelectedPageContext = pageContext;
            (pageContext.ViewModel as INotifyLoaded)?.OnLoaded();

            oldPageContext?.Dispose();
            return pageContext;
        }
    }
}
