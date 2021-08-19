using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace VagabondK.App
{
    /// <summary>
    /// 페이지 컨텍스트
    /// </summary>
    public class PageContext : IDisposable, INotifyPropertyChanged
    {
        private bool isDisposed;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                serviceScope?.Dispose();
                serviceScope = null;
            }
            GC.SuppressFinalize(this);
        }

        private IServiceScope serviceScope;
        private ViewProvider viewProvider;
        private Type viewModelType;
        private object viewModel;
        private Type viewType;
        private object view;
        private string title;
        private bool? result;
        private PageContext owner;

        /// <summary>
        /// 현재 페이지 컨텍스트 범위에서 서비스 개체를 검색하기 위한 메커니즘을 정의합니다.
        /// </summary>
        public IServiceProvider ServiceProvider { get => serviceScope?.ServiceProvider; }

        /// <summary>
        /// 속성 값이 변경될 때 발생합니다.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChanged 이벤트를 발생시킴.
        /// </summary>
        /// <param name="e">PropertyChanged 이벤트에 대한 데이터를 제공함.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        /// <summary>
        /// 뷰 모댈
        /// </summary>
        public object ViewModel { get => viewModel; private set => this.Set(ref viewModel, value, PropertyChanged); }

        /// <summary>
        /// 뷰
        /// </summary>
        public object View { get => view; private set => this.Set(ref view, value, PropertyChanged); }

        /// <summary>
        /// 페이지 제목
        /// </summary>
        public virtual string Title { get => title; set => this.Set(ref title, value, PropertyChanged); }

        /// <summary>
        /// 페이지 작업 결과
        /// </summary>
        public bool? Result { get => result; set => this.Set(ref result, value, PropertyChanged); }

        /// <summary>
        /// 페이지 소유자
        /// </summary>
        public PageContext Owner { get => owner; internal set => owner = value; }

        /// <summary>
        /// 뷰 새로고침, 뷰의 ServiceLifetime이 Transient일 때만 적용 가능
        /// </summary>
        public void ReloadView() => View = viewProvider?.CanReloadInternal(viewType) == true ? viewProvider.GetView(viewType) : null;

        internal void InitPageContext(IServiceScope serviceScope, Type viewModelType, Type viewType)
        {
            this.serviceScope = serviceScope;
            this.viewModelType = viewModelType;
            this.viewType = viewType;

            viewProvider = ServiceProvider?.GetRequiredService<ViewProvider>();

            if (this.viewModelType != null)
                ViewModel = ServiceProvider?.GetService(this.viewModelType);
            if (this.viewType != null) 
                View = viewProvider?.GetView(this.viewType);

            if (viewModel == null && view != null)
            {
                this.viewModelType = view.GetType().GetTypeInfo().GetCustomAttribute<ViewAttribute>()?.DefaultViewModelType;

                if (this.viewModelType != null)
                    ViewModel = ServiceProvider?.GetService(this.viewModelType);
            }
            else if (view == null)
            {
                this.viewType = viewModel.GetType().GetTypeInfo().GetCustomAttribute<ViewModelAttribute>()?.DefaultViewType;

                if (this.viewType != null)
                    View = viewProvider?.GetView(this.viewType);
            }
        }
    }

    /// <summary>
    /// 데이터를 포함하는 페이지 컨텍스트
    /// </summary>
    /// <typeparam name="TPageData">페이지 데이터 형식</typeparam>
    public class PageContext<TPageData> : PageContext where TPageData : IPageData
    {
        private TPageData pageData;

        /// <summary>
        /// 페이지 데이터
        /// </summary>
        public TPageData PageData
        {
            get => pageData;
            internal set
            {
                if (!Equals(pageData, value))
                {
                    if (pageData != null)
                        pageData.PropertyChanged -= OnPageDataPropertyChanged;

                    bool titleUpdated = pageData?.Title != value?.Title;

                    pageData = value;

                    if (pageData != null)
                        pageData.PropertyChanged += OnPageDataPropertyChanged;

                    if (titleUpdated)
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }

        private void OnPageDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPageData.Title))
            {
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        /// <summary>
        /// 페이지 제목
        /// </summary>
        public override string Title
        {
            get => PageData?.Title ?? base.Title;
            set
            {
                if (pageData == null)
                {
                    base.Title = value;
                }
                else if (pageData.Title != value)
                {
                    pageData.Title = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }
    }
}
