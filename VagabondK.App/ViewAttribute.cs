using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 뷰 특성
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
    public class ViewAttribute : ServiceDescriptionAttribute
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public ViewAttribute() : base(null, ServiceLifetime.Transient) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewType">뷰 형식</param>
        public ViewAttribute(Type viewType) : base(viewType, ServiceLifetime.Transient) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="priority">우선권(값이 클수록 우선권을 가짐)</param>
        public ViewAttribute(double priority) : base(null, ServiceLifetime.Transient, priority) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewType">뷰 형식</param>
        /// <param name="priority">우선권(값이 클수록 우선권을 가짐)</param>
        public ViewAttribute(Type viewType, double priority) : base(viewType, ServiceLifetime.Transient, priority) { }

        /// <summary>
        /// 기본 뷰 모델 형식
        /// </summary>
        public Type DefaultViewModelType { get; set; }
    }
}
