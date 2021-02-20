using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK
{
    /// <summary>
    /// 대화상자 서비스
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="pageContext">페이지 컨텍스트</param>
        /// <returns>대화상자 결과</returns>
        Task<bool?> ShowDialog(PageContext pageContext);
    }

    /// <summary>
    /// 대화상자 서비스 확장 메서드 모음
    /// </summary>
    public static class DialogExtensions
    {
        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider)
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType)
            => serviceProvider.ShowDialog(viewModelType, viewType, null as Action<PageContext>);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, Action<TViewModel> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, Action<TViewModel, object> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, pageContext.View));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel, object> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, pageContext.View));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, Action<TViewModel, TView> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, (TView)pageContext.View));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, Action<object, object> initializer)
            => serviceProvider.ShowDialog(viewModelType, viewType, (pageContext) => initializer?.Invoke(pageContext.ViewModel, pageContext.View));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, Action<PageContext> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), initializer);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<PageContext> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, initializer);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, Action<PageContext> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), initializer);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, Action<PageContext> initializer)
            => serviceProvider.ShowDialog(viewModelType, viewType, null, initializer);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, title);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, string title)
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title)
            => serviceProvider.ShowDialog(viewModelType, viewType, title, null as Action<PageContext>);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<TViewModel> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<TViewModel, object> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, pageContext.View));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<TViewModel> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<TViewModel, object> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, pageContext.View));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<TViewModel> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<TViewModel, TView> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, (TView)pageContext.View));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title, Action<object, object> initializer)
            => serviceProvider.ShowDialog(viewModelType, viewType, title, (pageContext) => initializer?.Invoke(pageContext.ViewModel, pageContext.View));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<PageContext> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, initializer);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<PageContext> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, initializer);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<PageContext> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, initializer);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title, Action<PageContext> initializer)
            => serviceProvider.ShowDialog(viewModelType, viewType, title, initializer, out _);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, out object viewModel)
        {
            var task = serviceProvider.ShowDialog(viewModelType, viewType, null as Action<PageContext>, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, pageContext.View), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, pageContext.View), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, Action<TViewModel, TView> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, (TView)pageContext.View), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, Action<object, object> initializer, out object viewModel)
        {
            var task = serviceProvider.ShowDialog(viewModelType, viewType, (pageContext) => initializer?.Invoke(pageContext.ViewModel, pageContext.View), out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, Action<PageContext> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), initializer, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<PageContext> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, initializer, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, Action<PageContext> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), initializer, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, Action<PageContext> initializer, out object viewModel)
        {
            var task = serviceProvider.ShowDialog(viewModelType, viewType, null, initializer, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, string title, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title, out object viewModel)
        {
            var task = serviceProvider.ShowDialog(viewModelType, viewType, title, null as Action<PageContext>, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, pageContext.View), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, pageContext.View), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<TViewModel, TView> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, (pageContext) => initializer?.Invoke((TViewModel)pageContext.ViewModel, (TView)pageContext.View), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title, Action<object, object> initializer, out object viewModel)
        {
            var task = serviceProvider.ShowDialog(viewModelType, viewType, title, (pageContext) => initializer?.Invoke(pageContext.ViewModel, pageContext.View), out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<PageContext> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, initializer, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<PageContext> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, initializer, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<PageContext> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, initializer, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title, Action<PageContext> initializer, out object viewModel)
        {
            using (var pageContext = serviceProvider.CreatePageContext(viewModelType, viewType, title))
            {
                initializer?.Invoke(pageContext);
                var dialog = pageContext.ServiceProvider.GetService<IDialog>();
                viewModel = pageContext.ViewModel;

                return dialog.ShowDialog(pageContext);
            }
        }
    }
}
