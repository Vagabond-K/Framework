﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK
{
    /// <summary>
    /// 서비스 공급자 확장 메서드 모음
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 응용프로그램 페이지 Scope 생성
        /// </summary>
        /// <param name="serviceProvider">페이지 공급자</param>
        /// <param name="viewModelTypeName">페이지 뷰 모델 형식 이름</param>
        /// <param name="viewTypeName">페이지 뷰 형식 이름</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>응용프로그램 페이지 Scope</returns>
        public static IServiceScope CreatePageScope(this IServiceProvider serviceProvider, string viewModelTypeName, string viewTypeName, string title) => CreatePageScope(serviceProvider, Type.GetType(viewModelTypeName), Type.GetType(viewTypeName), title);

        /// <summary>
        /// 응용프로그램 페이지 Scope 생성
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <param name="serviceProvider">페이지 공급자</param>
        /// <param name="viewTypeName">페이지 뷰 형식 이름</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>응용프로그램 페이지 Scope</returns>
        public static IServiceScope CreatePageScope<TViewModel>(this IServiceProvider serviceProvider, string viewTypeName, string title) => CreatePageScope(serviceProvider, typeof(TViewModel), Type.GetType(viewTypeName), title);

        /// <summary>
        /// 응용프로그램 페이지 Scope 생성
        /// </summary>
        /// <typeparam name="TViewModel">페이지 뷰 모델 형식</typeparam>
        /// <typeparam name="TView">페이지 뷰 형식</typeparam>
        /// <param name="serviceProvider">페이지 공급자</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>응용프로그램 페이지 Scope</returns>
        public static IServiceScope CreatePageScope<TViewModel, TView>(this IServiceProvider serviceProvider, string title) => CreatePageScope(serviceProvider, typeof(TViewModel), typeof(TView), title);

        /// <summary>
        /// 응용프로그램 페이지 Scope 생성
        /// </summary>
        /// <param name="serviceProvider">페이지 공급자</param>
        /// <param name="viewModelType">페이지 뷰 모델 형식</param>
        /// <param name="viewType">페이지 뷰 형식</param>
        /// <param name="title">페이지 제목</param>
        /// <returns>응용프로그램 페이지 Scope</returns>
        public static IServiceScope CreatePageScope(this IServiceProvider serviceProvider, Type viewModelType, Type viewType, string title)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            if (viewModelType == null) throw new ArgumentNullException(nameof(viewModelType));
            if (viewType == null) throw new ArgumentNullException(nameof(viewType));

            var owner = serviceProvider.GetService<PageContext>();

            var serviceScope = serviceProvider.CreateScope();
            var pageContext = serviceScope.ServiceProvider.GetService<PageContext>();

            pageContext.serviceScope = serviceScope;
            pageContext.ViewModel = serviceScope.ServiceProvider.GetService(viewModelType);
            pageContext.View = serviceScope.ServiceProvider.GetService(viewType);
            pageContext.Title = title;

            pageContext.Owner = owner;

            return serviceScope;
        }

        /// <summary>
        /// 응용프로그램 페이지 Scope 생성
        /// </summary>
        /// <typeparam name="TPageData">페이지 데이터 형식</typeparam>
        /// <param name="serviceProvider">서비스 공급자</param>
        /// <param name="pageData">페이지 데이터</param>
        /// <returns>응용프로그램 페이지 Scope</returns>
        public static IServiceScope CreatePageScope<TPageData>(this IServiceProvider serviceProvider, TPageData pageData) where TPageData : IPageData
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            if (pageData == null) throw new ArgumentNullException(nameof(pageData));
            var viewModelType = Type.GetType(pageData.ViewModelTypeName) ?? throw new TypeLoadException(pageData.ViewModelTypeName);
            var viewType = Type.GetType(pageData.ViewTypeName) ?? throw new TypeLoadException(nameof(pageData.ViewTypeName));

            var owner = serviceProvider.GetService<PageContext>();

            var serviceScope = serviceProvider.CreateScope();
            var pageContext = serviceScope.ServiceProvider.GetService<PageContext<TPageData>>();

            pageContext.serviceScope = serviceScope;
            pageContext.ViewModel = serviceScope.ServiceProvider.GetService(viewModelType);
            pageContext.View = serviceScope.ServiceProvider.GetService(viewType);
            pageContext.PageData = pageData;

            pageContext.Owner = owner;

            return serviceScope;
        }
    }
}
