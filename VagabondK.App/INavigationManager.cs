using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK.App
{
    /// <summary>
    /// 탐색 관리자 서비스
    /// </summary>
    public interface INavigationManager
    {
        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        Task<PageContext> Navigate(Type viewModelType, Type viewType, string title, Action<object, object> initializer, out object viewModel);
        /// <summary>
        /// 이전 페이지로 이동
        /// </summary>
        void GoBack();
        /// <summary>
        /// 이전 페이지로 이동 가능 여부를 가져옵니다.
        /// </summary>
        bool CanGoBack { get; }
        /// <summary>
        /// 탐색한 모든 이전 페이지를 삭제합니다.
        /// </summary>
        void ClearBackStack();
    }

    /// <summary>
    /// 탐색 관리자 서비스 확장 메서드 모음
    /// </summary>
    public static class NavigationManagerExtensions
    {
        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager)
            => navigationManager.Navigate(typeof(TViewModel), null as Type);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType)
            => navigationManager.Navigate(typeof(TViewModel), viewType);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager)
            => navigationManager.Navigate(typeof(TViewModel), typeof(TView));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType)
            => navigationManager.Navigate(viewModelType, null as Type, null as Action<object, object>);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, Type viewType)
            => navigationManager.Navigate(viewModelType, viewType, null as Action<object, object>);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Action<TViewModel> initializer)
            => navigationManager.Navigate(typeof(TViewModel), null as Type, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string title, Action<TViewModel> initializer)
            => navigationManager.Navigate(typeof(TViewModel), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Action<TViewModel, object> initializer)
            => navigationManager.Navigate(typeof(TViewModel), null as Type, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string title, Action<TViewModel, object> initializer)
            => navigationManager.Navigate(typeof(TViewModel), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, Action<TViewModel> initializer)
            => navigationManager.Navigate(typeof(TViewModel), viewType, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, Action<TViewModel, object> initializer)
            => navigationManager.Navigate(typeof(TViewModel), viewType, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager, Action<TViewModel, TView> initializer)
            => navigationManager.Navigate(typeof(TViewModel), typeof(TView), (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, (TView)view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, Action<object, object> initializer)
            => navigationManager.Navigate(viewModelType, null as Type, null, (viewModel, view) => initializer?.Invoke(viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, Type viewType, Action<object, object> initializer)
            => navigationManager.Navigate(viewModelType, viewType, null, (viewModel, view) => initializer?.Invoke(viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string title)
            => navigationManager.Navigate(typeof(TViewModel), null as Type, title);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string viewTypeName, string title)
            => navigationManager.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, string title)
            => navigationManager.Navigate(typeof(TViewModel), viewType, title);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager, string title)
            => navigationManager.Navigate(typeof(TViewModel), typeof(TView), title);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, string title)
            => navigationManager.Navigate(viewModelType, null as Type, title, out _);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, Type viewType, string title)
            => navigationManager.Navigate(viewModelType, viewType, title, out _);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string viewTypeName, string title, Action<TViewModel> initializer)
            => navigationManager.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string viewTypeName, string title, Action<TViewModel, object> initializer)
            => navigationManager.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, string title, Action<TViewModel> initializer)
            => navigationManager.Navigate(typeof(TViewModel), viewType, title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, string title, Action<TViewModel, object> initializer)
            => navigationManager.Navigate(typeof(TViewModel), viewType, title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager, string title, Action<TViewModel> initializer)
            => navigationManager.Navigate(typeof(TViewModel), typeof(TView), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager, string title, Action<TViewModel, TView> initializer)
            => navigationManager.Navigate(typeof(TViewModel), typeof(TView), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, (TView)view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, string title, Action<object, object> initializer)
            => navigationManager.Navigate(viewModelType, null as Type, title, (viewModel, view) => initializer?.Invoke(viewModel, view), out var viewModelResult);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, Type viewType, string title, Action<object, object> initializer)
            => navigationManager.Navigate(viewModelType, viewType, title, (viewModel, view) => initializer?.Invoke(viewModel, view), out var viewModelResult);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), null as Type, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string title, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), viewType, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), typeof(TView), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, out object viewModel)
        {
            var task = navigationManager.Navigate(viewModelType, null as Type, null as string, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, Type viewType, out object viewModel)
        {
            var task = navigationManager.Navigate(viewModelType, viewType, null as string, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), null as Type, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), null as Type, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), viewType, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), viewType, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager, Action<TViewModel, TView> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), typeof(TView), (vm, view) => initializer?.Invoke((TViewModel)vm, (TView)view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, Action<object, object> initializer, out object viewModel)
        {
            var task = navigationManager.Navigate(viewModelType, null as Type, null, (vm, view) => initializer?.Invoke(vm, view), out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, Type viewType, Action<object, object> initializer, out object viewModel)
        {
            var task = navigationManager.Navigate(viewModelType, viewType, null, (vm, view) => initializer?.Invoke(vm, view), out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string viewTypeName, string title, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, string title, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), viewType, title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager, string title, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), typeof(TView), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, string title, out object viewModel)
        {
            var task = navigationManager.Navigate(viewModelType, null as Type, title, null, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, Type viewType, string title, out object viewModel)
        {
            var task = navigationManager.Navigate(viewModelType, viewType, title, null, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string viewTypeName, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, string viewTypeName, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), viewType, title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this INavigationManager navigationManager, Type viewType, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), viewType, title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), typeof(TView), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this INavigationManager navigationManager, string title, Action<TViewModel, TView> initializer, out TViewModel viewModel)
        {
            var task = navigationManager.Navigate(typeof(TViewModel), typeof(TView), title, (vm, view) => initializer?.Invoke((TViewModel)vm, (TView)view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="navigationManager">탐색 관리자 서비스</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this INavigationManager navigationManager, Type viewModelType, string title, Action<object, object> initializer, out object viewModel)
        {
            var task = navigationManager.Navigate(viewModelType, null, title, initializer, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider)
            => serviceProvider.Navigate(typeof(TViewModel), null as Type);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType)
            => serviceProvider.Navigate(typeof(TViewModel), viewType);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider)
            => serviceProvider.Navigate(typeof(TViewModel), typeof(TView));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType)
            => serviceProvider.Navigate(viewModelType, null as Type, null as Action<object, object>);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Type viewType)
            => serviceProvider.Navigate(viewModelType, viewType, null as Action<object, object>);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Action<TViewModel> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), null as Type, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string title, Action<TViewModel> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Action<TViewModel, object> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), null as Type, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string title, Action<TViewModel, object> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), viewType, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel, object> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), viewType, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider, Action<TViewModel, TView> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), typeof(TView), (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, (TView)view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Action<object, object> initializer)
            => serviceProvider.Navigate(viewModelType, null as Type, null, (viewModel, view) => initializer?.Invoke(viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, Action<object, object> initializer)
            => serviceProvider.Navigate(viewModelType, viewType, null, (viewModel, view) => initializer?.Invoke(viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string title)
            => serviceProvider.Navigate(typeof(TViewModel), null as Type, title);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title)
            => serviceProvider.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title)
            => serviceProvider.Navigate(typeof(TViewModel), viewType, title);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider, string title)
            => serviceProvider.Navigate(typeof(TViewModel), typeof(TView), title);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, string title)
            => serviceProvider.Navigate(viewModelType, null as Type, title, out _);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title)
            => serviceProvider.Navigate(viewModelType, viewType, title, out _);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<TViewModel> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<TViewModel, object> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<TViewModel> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), viewType, title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<TViewModel, object> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), viewType, title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<TViewModel> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), typeof(TView), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<TViewModel, TView> initializer)
            => serviceProvider.Navigate(typeof(TViewModel), typeof(TView), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, (TView)view));

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, string title, Action<object, object> initializer)
            => serviceProvider.Navigate(viewModelType, null as Type, title, (viewModel, view) => initializer?.Invoke(viewModel, view), out var viewModelResult);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title, Action<object, object> initializer)
            => serviceProvider.Navigate(viewModelType, viewType, title, (viewModel, view) => initializer?.Invoke(viewModel, view), out var viewModelResult);

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), null as Type, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string title, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), viewType, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), typeof(TView), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, out object viewModel)
        {
            var task = serviceProvider.Navigate(viewModelType, null as Type, null as string, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, out object viewModel)
        {
            var task = serviceProvider.Navigate(viewModelType, viewType, null as string, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), null as Type, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), null as Type, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), viewType, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), viewType, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider, Action<TViewModel, TView> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), typeof(TView), (vm, view) => initializer?.Invoke((TViewModel)vm, (TView)view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Action<object, object> initializer, out object viewModel)
        {
            var task = serviceProvider.Navigate(viewModelType, null as Type, null, (vm, view) => initializer?.Invoke(vm, view), out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, Action<object, object> initializer, out object viewModel)
        {
            var task = serviceProvider.Navigate(viewModelType, viewType, null, (vm, view) => initializer?.Invoke(vm, view), out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), viewType, title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider, string title, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), typeof(TView), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, string title, out object viewModel)
        {
            var task = serviceProvider.Navigate(viewModelType, null as Type, title, null, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title, out object viewModel)
        {
            var task = serviceProvider.Navigate(viewModelType, viewType, title, null, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="viewTypeName">탐색 대상 뷰 형식 이름</param>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), viewType, title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel>(this IServiceProvider serviceProvider, Type viewType, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), viewType, title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), typeof(TView), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <typeparam name="TViewModel">탐색 대상 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">탐색 대상 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate<TViewModel, TView>(this IServiceProvider serviceProvider, string title, Action<TViewModel, TView> initializer, out TViewModel viewModel)
        {
            var task = serviceProvider.Navigate(typeof(TViewModel), typeof(TView), title, (vm, view) => initializer?.Invoke((TViewModel)vm, (TView)view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="viewType">탐색 대상 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title, Action<object, object> initializer, out object viewModel)
        {
            var task = serviceProvider.GetRequiredService<INavigationManager>().Navigate(viewModelType, viewType, title, initializer, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 탐색
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">탐색 대상 뷰 모델 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">탐색 후 뷰 모델 결과</param>
        /// <returns>페이지 컨텍스트</returns>
        public static Task<PageContext> Navigate(this IServiceProvider serviceProvider, Type viewModelType, string title, Action<object, object> initializer, out object viewModel)
        {
            var task = serviceProvider.GetRequiredService<INavigationManager>().Navigate(viewModelType, null, title, initializer, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }
    }
}
