using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK.App
{
    /// <summary>
    /// 대화상자 서비스
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        Task<bool?> ShowDialog(Type viewModelType, Type viewType, string title, Action<object, object> initializer, out object viewModel);
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
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName)
            => dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType)
            => dialog.ShowDialog(typeof(TViewModel), viewType);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog)
            => dialog.ShowDialog(typeof(TViewModel), typeof(TView));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IDialog dialog, Type viewModelType, Type viewType)
            => dialog.ShowDialog(viewModelType, viewType, null as Action<object, object>);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, Action<TViewModel> initializer)
            => dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, Action<TViewModel, object> initializer)
            => dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, Action<TViewModel> initializer)
            => dialog.ShowDialog(typeof(TViewModel), viewType, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, Action<TViewModel, object> initializer)
            => dialog.ShowDialog(typeof(TViewModel), viewType, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog, Action<TViewModel, TView> initializer)
            => dialog.ShowDialog(typeof(TViewModel), typeof(TView), (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, (TView)view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IDialog dialog, Type viewModelType, Type viewType, Action<object, object> initializer)
            => dialog.ShowDialog(viewModelType, viewType, null, (viewModel, view) => initializer?.Invoke(viewModel, view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, string title)
            => dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, string title)
            => dialog.ShowDialog(typeof(TViewModel), viewType, title);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog, string title)
            => dialog.ShowDialog(typeof(TViewModel), typeof(TView), title);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IDialog dialog, Type viewModelType, Type viewType, string title)
            => dialog.ShowDialog(viewModelType, viewType, title, out _);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, string title, Action<TViewModel> initializer)
            => dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, string title, Action<TViewModel, object> initializer)
            => dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, string title, Action<TViewModel> initializer)
            => dialog.ShowDialog(typeof(TViewModel), viewType, title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, string title, Action<TViewModel, object> initializer)
            => dialog.ShowDialog(typeof(TViewModel), viewType, title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog, string title, Action<TViewModel> initializer)
            => dialog.ShowDialog(typeof(TViewModel), typeof(TView), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog, string title, Action<TViewModel, TView> initializer)
            => dialog.ShowDialog(typeof(TViewModel), typeof(TView), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, (TView)view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IDialog dialog, Type viewModelType, Type viewType, string title, Action<object, object> initializer)
            => dialog.ShowDialog(viewModelType, viewType, title, (viewModel, view) => initializer?.Invoke(viewModel, view), out var viewModelResult);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), viewType, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), typeof(TView), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IDialog dialog, Type viewModelType, Type viewType, out object viewModel)
        {
            var task = dialog.ShowDialog(viewModelType, viewType, null as string, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), viewType, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), viewType, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog, Action<TViewModel, TView> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), typeof(TView), (vm, view) => initializer?.Invoke((TViewModel)vm, (TView)view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IDialog dialog, Type viewModelType, Type viewType, Action<object, object> initializer, out object viewModel)
        {
            var task = dialog.ShowDialog(viewModelType, viewType, null, (vm, view) => initializer?.Invoke(vm, view), out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, string title, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, string title, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), viewType, title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog, string title, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), typeof(TView), title, out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="title">제목</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IDialog dialog, Type viewModelType, Type viewType, string title, out object viewModel)
        {
            var task = dialog.ShowDialog(viewModelType, viewType, title, null, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, string viewTypeName, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), viewType, title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IDialog dialog, Type viewType, string title, Action<TViewModel, object> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), viewType, title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog, string title, Action<TViewModel> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), typeof(TView), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="dialog">대화상자 서비스</param>
        /// <param name="title">제목</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <param name="viewModel">대화상자 표시 후 뷰 모델 결과</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IDialog dialog, string title, Action<TViewModel, TView> initializer, out TViewModel viewModel)
        {
            var task = dialog.ShowDialog(typeof(TViewModel), typeof(TView), title, (vm, view) => initializer?.Invoke((TViewModel)vm, (TView)view), out var viewModelResult);
            viewModel = (TViewModel)viewModelResult;
            return task;
        }



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
            => serviceProvider.ShowDialog(viewModelType, viewType, null as Action<object, object>);

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, Action<TViewModel> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewTypeName">대화상자 뷰 형식 이름</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, Action<TViewModel, object> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel>(this IServiceProvider serviceProvider, Type viewType, Action<TViewModel, object> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <typeparam name="TViewModel">대화상자 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">대화상자 뷰 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog<TViewModel, TView>(this IServiceProvider serviceProvider, Action<TViewModel, TView> initializer)
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, (TView)view));

        /// <summary>
        /// 대화상자 표시
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">대화상자 뷰 모델 형식</param>
        /// <param name="viewType">대화상자 뷰 형식</param>
        /// <param name="initializer">초기화 대리자</param>
        /// <returns>대화상자 결과</returns>
        public static Task<bool?> ShowDialog(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, Action<object, object> initializer)
            => serviceProvider.ShowDialog(viewModelType, viewType, null, (viewModel, view) => initializer?.Invoke(viewModel, view));

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
            => serviceProvider.ShowDialog(viewModelType, viewType, title, out _);

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
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

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
            => serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

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
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

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
            => serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, view));

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
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel));

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
            => serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, (viewModel, view) => initializer?.Invoke((TViewModel)viewModel, (TView)view));

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
            => serviceProvider.ShowDialog(viewModelType, viewType, title, (viewModel, view) => initializer?.Invoke(viewModel, view), out var viewModelResult);

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
            var task = serviceProvider.ShowDialog(viewModelType, viewType, null as string, out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), (vm, view) => initializer?.Invoke((TViewModel)vm, (TView)view), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(viewModelType, viewType, null, (vm, view) => initializer?.Invoke(vm, view), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(viewModelType, viewType, title, null, out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), Type.GetType(viewTypeName), title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), viewType, title, (vm, view) => initializer?.Invoke((TViewModel)vm, view), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, (vm, view) => initializer?.Invoke((TViewModel)vm), out var viewModelResult);
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
            var task = serviceProvider.ShowDialog(typeof(TViewModel), typeof(TView), title, (vm, view) => initializer?.Invoke((TViewModel)vm, (TView)view), out var viewModelResult);
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
            var task = serviceProvider.GetService<IDialog>().ShowDialog(viewModelType, viewType, title, initializer, out var viewModelResult);
            viewModel = viewModelResult;
            return task;
        }
    }
}
