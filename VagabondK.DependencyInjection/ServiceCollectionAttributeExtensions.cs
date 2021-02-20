using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System
{
    /// <summary>
    /// 특성 기반 서비스 관리용 확장 메서드 모음
    /// </summary>
    public static class ServiceCollectionAttributeExtensions
    {
        /// <summary>
        /// 어셈블리에 정의된 ServiceDescriptionAttribute 특성을 가진 클래스들을 자동으로 서비스 컬렉션에 추가 함.
        /// ServiceDescriptionAttribute 특성의 ServiceType이 같을 경우, Priority 값이 가장 큰 것을 서비스 컬렉션에 추가 함.
        /// </summary>
        /// <param name="services">서비스 컬렉션</param>
        /// <param name="assembly">어셈블리</param>
        /// <returns>서비스 컬렉션</returns>
        public static IServiceCollection AddServices(this IServiceCollection services, Assembly assembly)
        {
            if (assembly == null) return services;

            var serviceDictionary = services.Where(serviceDescriptor => serviceDescriptor is PriorityServiceDescriptor)
                .ToDictionary(serviceDescriptor => serviceDescriptor.ServiceType);
            
            foreach (var newServiceDescriptor in assembly.GetServiceDescriptors())
            {
                if (serviceDictionary.TryGetValue(newServiceDescriptor.ServiceType, out var oldServiceDescriptor))
                {
                    if ((oldServiceDescriptor is PriorityServiceDescriptor oldPriorityServiceDescriptor
                        && oldPriorityServiceDescriptor.ImplementationType != newServiceDescriptor.ImplementationType
                        && (oldPriorityServiceDescriptor.Priority < newServiceDescriptor.Priority
                        || oldPriorityServiceDescriptor.ImplementationType?.IsAssignableFrom(newServiceDescriptor.ImplementationType) == true))
                        || (oldServiceDescriptor.ImplementationType != newServiceDescriptor.ImplementationType
                        && oldServiceDescriptor.ImplementationType?.IsAssignableFrom(newServiceDescriptor.ImplementationType) == true))
                    {
                        services.Replace(newServiceDescriptor);
                    }
                }
                else
                {
                    services.Add(newServiceDescriptor);
                    serviceDictionary[newServiceDescriptor.ServiceType] = newServiceDescriptor;
                }
            }

            return services;
        }

        /// <summary>
        /// 어셈블리에 정의된 ServiceDescriptionAttribute 특성을 가진 클래스들을 자동으로 서비스 컬렉션에 추가 함.
        /// ServiceDescriptionAttribute 특성의 ServiceType이 같을 경우, Priority 값이 가장 큰 것을 서비스 컬렉션에 추가 함.
        /// </summary>
        /// <param name="services">서비스 컬렉션</param>
        /// <param name="assemblyName">어셈블리 명</param>
        /// <returns>서비스 컬렉션</returns>
        public static IServiceCollection AddServices(this IServiceCollection services, string assemblyName)
            => services?.AddServices(Assembly.Load(new AssemblyName(assemblyName)));

        private static IEnumerable<PriorityServiceDescriptor> GetServiceDescriptors(this Assembly assembly)
        {
            if (assembly == null) yield break;

            foreach (var typeInfo in assembly.DefinedTypes)
            {
                if (!typeInfo.IsAbstract && !typeInfo.IsInterface)
                {
                    foreach (ServiceDescriptionAttribute descriptionAttribute in typeInfo.GetCustomAttributes<ServiceDescriptionAttribute>())
                    {
                        var constructors = typeInfo.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                            .Where(constructor => constructor.GetCustomAttribute<ServiceConstructorAttribute>() != null).ToArray();

                        if (constructors.Length > 1)
                        {
                            throw new Exception("'service constructor' must be one.");
                        }
                        else if (constructors.Length == 1)
                        {
                            var constructor = constructors[0];
                            yield return new PriorityServiceDescriptor(descriptionAttribute.ServiceType ?? typeInfo.AsType(), typeInfo.AsType(),
                                serviceProvider => constructor.Invoke(constructor.GetParameters().OrderBy(
                                    parameterInfo => parameterInfo.Position).Select(
                                    parameterInfo => serviceProvider.GetService(parameterInfo.ParameterType)).ToArray())
                                , descriptionAttribute.Lifetime, descriptionAttribute.Priority);
                        }
                        else
                        {
                            yield return new PriorityServiceDescriptor(descriptionAttribute.ServiceType ?? typeInfo.AsType(), typeInfo.AsType(), descriptionAttribute.Lifetime, descriptionAttribute.Priority);
                        }
                    }
                }
            }
        }

        class PriorityServiceDescriptor : ServiceDescriptor
        {
            public PriorityServiceDescriptor(Type serviceType, object instance, double priority) : base(serviceType, instance)
            {
                Priority = priority;
            }

            public PriorityServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime, double priority) : base(serviceType, implementationType, lifetime)
            {
                Priority = priority;
            }

            public PriorityServiceDescriptor(Type serviceType, Type implementationType, Func<IServiceProvider, object> factory, ServiceLifetime lifetime, double priority) : base(serviceType, factory, lifetime)
            {
                this.implementationType = implementationType;
                Priority = priority;
            }

            private readonly Type implementationType;

            public double Priority { get; }
            public new Type ImplementationType { get => base.ImplementationType ?? implementationType; }
        }

    }
}
