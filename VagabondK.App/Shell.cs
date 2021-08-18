using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace VagabondK.App
{
    /// <summary>
    /// 쉘
    /// </summary>
    public abstract class Shell : INotifyPropertyChanged
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="services">서비스 컬렉션</param>
        protected Shell(IServiceCollection services)
        {
            addedShellServices = services ?? new ServiceCollection();

            defaultServices = new ServiceCollection();
            defaultServices.AddSingleton(this);
            defaultServices.AddSingleton(GetType(), this);
            defaultServices.AddScoped<PageContext>();
        }

        internal readonly IServiceCollection defaultServices;
        private readonly IServiceCollection addedShellServices;
        private IServiceProvider serviceProvider;

        internal IServiceProvider CreateServiceProviderCore(IServiceCollection services)
        {
            var serviceProviderFactory = new DefaultServiceProviderFactory(new ServiceProviderOptions());

            IServiceCollection builder = new ServiceCollection
            {
                defaultServices.Concat(addedShellServices).Concat(services)
            };

            return serviceProviderFactory.CreateServiceProvider(serviceProviderFactory.CreateBuilder(builder));
        }

        internal IServiceProvider GetShellServiceProvider()
        {
            if (serviceProvider == null)
                serviceProvider = CreateServiceProviderCore(new ServiceCollection());
            return serviceProvider;
        }

        private object icon;
        private object title;
        private PageContext selectedPageContext;

        /// <summary>
        /// 속성 값이 변경될 때 발생합니다.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 쉘 서비스 공급자
        /// </summary>
        public IServiceProvider ShellServiceProvider { get => GetShellServiceProvider(); }

        /// <summary>
        /// 아이콘
        /// </summary>
        public object Icon { get => icon; set => this.Set(ref icon, value, PropertyChanged); }

        /// <summary>
        /// 제목
        /// </summary>
        public object Title { get => title; set => this.Set(ref title, value, PropertyChanged); }

        /// <summary>
        /// 선택된 페이지
        /// </summary>
        public PageContext SelectedPageContext { get => selectedPageContext; set => this.Set(ref selectedPageContext, value, PropertyChanged); }

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="viewModelTypeName">페이지 뷰 모델 형식 이름</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(string viewModelTypeName)
            => OpenPage(string.IsNullOrWhiteSpace(viewModelTypeName) ? null : Type.GetType(viewModelTypeName), null);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel>()
            => OpenPage(typeof(TViewModel), null, null);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(Type viewModelType)
            => OpenPage(viewModelType, null, null);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="viewModelTypeName">페이지 뷰 모델 형식 이름</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(string viewModelTypeName, string title)
            => OpenPage(string.IsNullOrWhiteSpace(viewModelTypeName) ? null : Type.GetType(viewModelTypeName), null, title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel>(string title)
            => OpenPage(typeof(TViewModel), null, title);


        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">페이지 뷰 형식</typeparam>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel, TView>()
            => OpenPage(typeof(TViewModel), typeof(TView), null);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <param name="viewType">페이지 뷰 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(Type viewModelType, Type viewType)
            => OpenPage(viewModelType, viewType, null);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="viewModelTypeName">페이지 뷰 모델 형식 이름</param>
        /// <param name="viewTypeName">페이지 뷰 형식 이름</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(string viewModelTypeName, string viewTypeName, string title)
            => OpenPage(string.IsNullOrWhiteSpace(viewModelTypeName) ? null : Type.GetType(viewModelTypeName), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <param name="viewTypeName">페이지 뷰 형식 이름</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel>(string viewTypeName, string title)
            => OpenPage(typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">페이지 뷰 형식</typeparam>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel, TView>(string title)
            => OpenPage(typeof(TViewModel), typeof(TView), title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <param name="viewType">페이지 뷰 형식</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public abstract Task<PageContext> OpenPage(Type viewModelType, Type viewType, string title);
    }

    /// <summary>
    /// 페이지 데이터가 포함된 쉘
    /// </summary>
    /// <typeparam name="TPageData">페이지 데이터</typeparam>
    public abstract class Shell<TPageData> : Shell where TPageData : IPageData
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="services">서비스 컬렉션</param>
        protected Shell(IServiceCollection services) : base(services)
        {
            defaultServices.AddSingleton(this);
            defaultServices.AddScoped<PageContext<TPageData>>();
            defaultServices.AddScoped<PageContext>(serviceProvider => serviceProvider.GetRequiredService<PageContext<TPageData>>());
        }

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="pageData">페이지 데이터</param>
        /// <returns>페이지 컨텍스트</returns>
        public abstract Task<PageContext<TPageData>> OpenPage(TPageData pageData);
    }
}
