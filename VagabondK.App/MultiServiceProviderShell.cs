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
    /// 다중 서비스 공급자용 쉘
    /// </summary>
    /// <typeparam name="TServiceProviderKey">서비스 공급자 키 형식</typeparam>
    /// <typeparam name="TPageData">페이지 데이터 형식</typeparam>
    public abstract class MultiServiceProviderShell<TServiceProviderKey, TPageData> : Shell<TPageData>, IReadOnlyDictionary<TServiceProviderKey, IServiceProvider>
        where TServiceProviderKey : class
        where TPageData : IPageData
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="services">추가 서비스 목록</param>
        protected MultiServiceProviderShell(IServiceCollection services) : base(services)
        {
            services?.AddSingleton(this);
        }

        private readonly Dictionary<TServiceProviderKey, IServiceProvider> serviceProviders = new Dictionary<TServiceProviderKey, IServiceProvider>();


        /// <summary>
        /// 서비스 공급자 생성, 다중 서비스 공급자 기반의 프로그램일 경우에 사용함
        /// </summary>
        /// <param name="key">키</param>
        /// <param name="services">서비스 컬렉션</param>
        /// <returns>서비스 공급자</returns>
        public IServiceProvider CreateServiceProvider(TServiceProviderKey key, IServiceCollection services)
        {
            lock (serviceProviders)
            {
                if (serviceProviders.ContainsKey(key)) throw new Exception();

                IServiceCollection builder = new ServiceCollection { services };

                builder.AddSingleton(key);

                var serviceProvider = CreateServiceProviderCore(builder);
                serviceProviders[key] = serviceProvider;

                return serviceProvider;
            }
        }

        /// <summary>
        /// 서비스 공급자 제거
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <returns>제거 결과</returns>
        public bool Remove(TServiceProviderKey key) { lock (serviceProviders) return serviceProviders.Remove(key); }

        /// <summary>
        /// 서비스 공급자 키 목록
        /// </summary>
        public ICollection<TServiceProviderKey> Keys => serviceProviders.Keys;

        /// <summary>
        /// 생성된 서비스 공급자 목록
        /// </summary>
        public ICollection<IServiceProvider> Values => serviceProviders.Values;

        /// <summary>
        /// 서비스 공급자 개수
        /// </summary>
        public int Count { get { lock (serviceProviders) return serviceProviders.Count; } }

        /// <summary>
        /// 서비스 공급자 가져오기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <returns>서비스 공급자</returns>
        public IServiceProvider this[TServiceProviderKey key] { get => serviceProviders[key]; }

        /// <summary>
        /// 서비스 공급자 키 존재 여부 확인
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <returns>서비스 공급자 키 존재 여부</returns>
        public bool ContainsKey(TServiceProviderKey key) => serviceProviders.ContainsKey(key);

        /// <summary>
        /// 서비스 공급자 가져오기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="value">서비스 공급자</param>
        /// <returns>서비스 공급자 존재 여부</returns>
        public bool TryGetValue(TServiceProviderKey key, out IServiceProvider value) => serviceProviders.TryGetValue(key, out value);

        /// <summary>
        /// 키 및 서비스 공급자 열거
        /// </summary>
        /// <returns>열거자</returns>
        public IEnumerator<KeyValuePair<TServiceProviderKey, IServiceProvider>> GetEnumerator()
            => serviceProviders.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IEnumerable<TServiceProviderKey> IReadOnlyDictionary<TServiceProviderKey, IServiceProvider>.Keys => Keys;
        IEnumerable<IServiceProvider> IReadOnlyDictionary<TServiceProviderKey, IServiceProvider>.Values => Values;


        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="viewModelTypeName">페이지 뷰 모델 형식 이름</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(TServiceProviderKey key, string viewModelTypeName)
            => OpenPage(key, string.IsNullOrWhiteSpace(viewModelTypeName) ? null : Type.GetType(viewModelTypeName), null as Type);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <param name="key">서비스 공급자 키</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel>(TServiceProviderKey key)
            => OpenPage(key, typeof(TViewModel), null as Type);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(TServiceProviderKey key, Type viewModelType)
            => OpenPage(key, viewModelType, null, null);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="viewModelTypeName">페이지 뷰 모델 형식 이름</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(TServiceProviderKey key, string viewModelTypeName, string title)
            => OpenPage(key, string.IsNullOrWhiteSpace(viewModelTypeName) ? null : Type.GetType(viewModelTypeName), null, title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel>(TServiceProviderKey key, string title)
            => OpenPage(key, typeof(TViewModel), null, title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(TServiceProviderKey key, Type viewModelType, string title)
            => OpenPage(serviceProviders[key], viewModelType, null, title);



        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">페이지 뷰 형식</typeparam>
        /// <param name="key">서비스 공급자 키</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel, TView>(TServiceProviderKey key)
            => OpenPage(key, typeof(TViewModel), typeof(TView));

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <param name="viewType">페이지 뷰 형식</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(TServiceProviderKey key, Type viewModelType, Type viewType)
            => OpenPage(key, viewModelType, viewType, null);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="viewModelTypeName">페이지 뷰 모델 형식 이름</param>
        /// <param name="viewTypeName">페이지 뷰 형식 이름</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(TServiceProviderKey key, string viewModelTypeName, string viewTypeName, string title)
            => OpenPage(key, string.IsNullOrWhiteSpace(viewModelTypeName) ? null : Type.GetType(viewModelTypeName), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="viewTypeName">페이지 뷰 형식 이름</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel>(TServiceProviderKey key, string viewTypeName, string title)
            => OpenPage(key, typeof(TViewModel), string.IsNullOrWhiteSpace(viewTypeName) ? null : Type.GetType(viewTypeName), title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">페이지 뷰 형식</typeparam>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage<TViewModel, TView>(TServiceProviderKey key, string title)
            => OpenPage(key, typeof(TViewModel), typeof(TView), title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <param name="viewType">페이지 뷰 형식</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext> OpenPage(TServiceProviderKey key, Type viewModelType, Type viewType, string title)
            => OpenPage(serviceProviders[key], viewModelType, viewType, title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="key">서비스 공급자 키</param>
        /// <param name="pageData">페이지 데이터</param>
        /// <returns>페이지 컨텍스트</returns>
        public Task<PageContext<TPageData>> OpenPage(TServiceProviderKey key, TPageData pageData)
            => OpenPage(serviceProviders[key], pageData);


        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <param name="viewType">페이지 뷰 형식</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>페이지 컨텍스트</returns>
        protected abstract Task<PageContext> OpenPage(IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title);

        /// <summary>
        /// 페이지 열기
        /// </summary>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="pageData">페이지 데이터</param>
        /// <returns>페이지 컨텍스트</returns>
        protected abstract Task<PageContext<TPageData>> OpenPage(IServiceProvider serviceProvider, TPageData pageData);

    }
}
