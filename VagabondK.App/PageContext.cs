using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace VagabondK
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

        internal IServiceScope serviceScope;

        private object view;
        private object viewModel;
        private string title;
        private bool? result;
        private PageContext owner;

        /// <summary>
        /// 속성 값이 변경될 때 발생합니다.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 뷰 모댈
        /// </summary>
        public object ViewModel { get => viewModel; internal set => this.Set(ref viewModel, value, PropertyChanged); }

        /// <summary>
        /// 뷰
        /// </summary>
        public object View { get => view; internal set => this.Set(ref view, value, PropertyChanged); }

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
    }

    /// <summary>
    /// 데이터를 포함하는 페이지 컨텍스트
    /// </summary>
    /// <typeparam name="TPageData">페이지 데이터 형식</typeparam>
    public class PageContext<TPageData> : PageContext where TPageData : IPageData
    {
        /// <summary>
        /// 속성 값이 변경될 때 발생합니다.
        /// </summary>
        public override event PropertyChangedEventHandler PropertyChanged;

        private TPageData pageData;

        /// <summary>
        /// 페이지 데이터
        /// </summary>
        public TPageData PageData
        {
            get => pageData;
            internal set
            {
                if (pageData != null && !Equals(pageData, value))
                    pageData.PropertyChanged -= OnPageDataPropertyChanged;

                bool titleUpdated = pageData?.Title != value?.Title;
                if (this.Set(ref pageData, value, PropertyChanged) && titleUpdated)
                {
                    if (pageData != null)
                        pageData.PropertyChanged += OnPageDataPropertyChanged;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }

        private void OnPageDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPageData.Title))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }
    }
}
