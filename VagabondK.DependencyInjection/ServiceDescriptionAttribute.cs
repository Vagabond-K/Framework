using Microsoft.Extensions.DependencyInjection;

namespace System
{
    /// <summary>
    /// 서비스 기술 특성
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
    public class ServiceDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public ServiceDescriptionAttribute() : this(null, ServiceLifetime.Scoped) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="lifetime">서비스 수명</param>
        public ServiceDescriptionAttribute(ServiceLifetime lifetime) : this(null, lifetime) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="serviceType">서비스 형식</param>
        public ServiceDescriptionAttribute(Type serviceType) : this(serviceType, ServiceLifetime.Scoped) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="serviceType">서비스 형식</param>
        /// <param name="lifetime">서비스 수명</param>
        public ServiceDescriptionAttribute(Type serviceType, ServiceLifetime lifetime) : this(serviceType, lifetime, 0) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="serviceType">서비스 형식</param>
        /// <param name="priority">우선권(값이 클수록 우선권을 가짐)</param>
        public ServiceDescriptionAttribute(Type serviceType, double priority) : this(serviceType, ServiceLifetime.Scoped, priority) { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="serviceType">서비스 형식</param>
        /// <param name="lifetime">서비스 수명</param>
        /// <param name="priority">우선권(값이 클수록 우선권을 가짐)</param>
        public ServiceDescriptionAttribute(Type serviceType, ServiceLifetime lifetime, double priority)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
            Priority = priority;
        }

        /// <summary>
        /// 서비스 형식
        /// </summary>
        public Type ServiceType { get; }
        /// <summary>
        /// 서비스 수명
        /// </summary>
        public ServiceLifetime Lifetime { get; } = ServiceLifetime.Scoped;
        /// <summary>
        /// 우선권(값이 클수록 우선권을 가짐)
        /// </summary>
        public double Priority { get; }
    }
}
