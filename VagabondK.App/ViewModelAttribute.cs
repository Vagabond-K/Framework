using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 뷰 모델 특성
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
    public class ViewModelAttribute : ServiceDescriptionAttribute
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public ViewModelAttribute() : this(null, ServiceLifetime.Scoped) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="lifetime">뷰 모델 수명</param>
        public ViewModelAttribute(ServiceLifetime lifetime) : this(null, lifetime) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewModelType">뷰 모델 형식</param>
        public ViewModelAttribute(Type viewModelType) : this(viewModelType, ServiceLifetime.Scoped) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="priority">우선권(값이 클수록 우선권을 가짐)</param>
        public ViewModelAttribute(double priority) : this(null, ServiceLifetime.Scoped, priority) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewModelType">뷰 모델 형식</param>
        /// <param name="lifetime">뷰 모델 수명</param>
        public ViewModelAttribute(Type viewModelType, ServiceLifetime lifetime) : this(viewModelType, lifetime, 0) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewModelType">뷰 모델 형식</param>
        /// <param name="priority">우선권(값이 클수록 우선권을 가짐)</param>
        public ViewModelAttribute(Type viewModelType, double priority) : this(viewModelType, ServiceLifetime.Scoped, priority) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="lifetime">뷰 모델 수명</param>
        /// <param name="priority">우선권(값이 클수록 우선권을 가짐)</param>
        public ViewModelAttribute(ServiceLifetime lifetime, double priority) : this(null, lifetime, priority) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewModelType">뷰 모델 형식</param>
        /// <param name="lifetime">뷰 모델 수명</param>
        /// <param name="priority">우선권(값이 클수록 우선권을 가짐)</param>
        public ViewModelAttribute(Type viewModelType, ServiceLifetime lifetime, double priority) : base(viewModelType, lifetime, priority) { }

        /// <summary>
        /// 기본 뷰 형식
        /// </summary>
        public Type DefaultViewType { get; set; }
    }
}
