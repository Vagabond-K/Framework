using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
    /// <summary>
    /// PropertyChanging과 PropertyChanged 이벤트 처리에 대한 기본적인 동작을 제공하는 클래스
    /// </summary>
    public abstract class NotifyPropertyChangeObject : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// 생성자
        /// </summary>
        protected NotifyPropertyChangeObject()
        {
            eventSource = this;
            eventSourceType = eventSource.GetType();
        }

        internal NotifyPropertyChangeObject(object eventSource)
        {
            this.eventSource = eventSource ?? this;
            eventSourceType = this.eventSource.GetType();
        }

        private readonly object eventSource = null;
        private readonly Type eventSourceType = null;

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<string, PropertyValue> propertyValues = new Dictionary<string, PropertyValue>();
        private const string argumentNullOrWhiteSpaceExceptionMessage = "propertyName cannot be empty or white space.";

        /// <summary>
        /// PropertyChanging 이벤트를 발생시킴.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual bool OnPropertyChanging(QueryPropertyChangingEventArgs e)
        {
            PropertyChanging?.Invoke(eventSource, e);
            return !e.IsCanceled;
        }

        /// <summary>
        /// PropertyChanged 이벤트를 발생시킴.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
            => PropertyChanged?.Invoke(eventSource, e);

        /// <summary>
        /// 속성의 형식을 반환
        /// </summary>
        /// <param name="propertyName">속성 명</param>
        /// <returns>속성 형식</returns>
        protected Type GetPropertyType(string propertyName)
        {
            var propertyInfo = eventSourceType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (propertyInfo == null) throw new ArgumentOutOfRangeException(nameof(propertyName));
            return propertyInfo.PropertyType;
        }

        /// <summary>
        /// 속성 값 가져오기
        /// </summary>
        /// <typeparam name="TProperty">속성 형식</typeparam>
        /// <param name="factory">속성 초기화 동작</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>속성 값</returns>
        protected TProperty Get<TProperty>(Func<TProperty> factory, [CallerMemberName]string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException(argumentNullOrWhiteSpaceExceptionMessage, nameof(propertyName));

            if (propertyValues.TryGetValue(propertyName, out var propertyValue))
            {
                return propertyValue.GetValue<TProperty>();
            }
            else
            {
                var propertyType = GetPropertyType(propertyName);

                PropertyValue newPropertyValue =
                    typeof(TProperty) == propertyType
                    ? new PropertyValue<TProperty>()
                    : (PropertyValue)Activator.CreateInstance(typeof(PropertyValue<>).MakeGenericType(propertyType));

                newPropertyValue.type = propertyType;

                TProperty newValue = default;
                if (factory != null)
                    newValue = factory.Invoke();

                newPropertyValue.SetValue(newValue);
                propertyValues[propertyName] = newPropertyValue;

                return newValue;
            }
        }

        /// <summary>
        /// 속성 값 가져오기
        /// </summary>
        /// <typeparam name="TProperty">속성 형식</typeparam>
        /// <param name="defaultValue">초기 기본 값</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>속성 값</returns>
        protected TProperty Get<TProperty>(TProperty defaultValue = default, [CallerMemberName]string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException(argumentNullOrWhiteSpaceExceptionMessage, nameof(propertyName));

            if (propertyValues.TryGetValue(propertyName, out var propertyValue))
            {
                TProperty result;
                if (propertyValue is PropertyValue<TProperty> genericValue)
                    result = genericValue.value;
                else
                    result = propertyValue.GetValue<TProperty>();

                if (result == null)
                    return defaultValue;
                else
                    return result;
            }
            else
            {
                var propertyType = GetPropertyType(propertyName);

                if (typeof(TProperty) == propertyType)
                {
                    propertyValue = new PropertyValue<TProperty>()
                    {
                        type = propertyType,
                        value = defaultValue
                    };
                }
                else
                {
                    propertyValue = (PropertyValue)Activator.CreateInstance(typeof(PropertyValue<>).MakeGenericType(propertyType));
                    propertyValue.type = propertyType;
                    propertyValue.SetValue(defaultValue);
                }
                propertyValues[propertyName] = propertyValue;

                return defaultValue;
            }
        }

        /// <summary>
        /// 속성 값 설정하기
        /// </summary>
        /// <typeparam name="TProperty">속성 형식</typeparam>
        /// <param name="value">설정할 값</param>
        /// <param name="propertyName">속성 명</param>
        /// <returns>설정 여부</returns>
        protected bool Set<TProperty>(TProperty value, [CallerMemberName]string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException(argumentNullOrWhiteSpaceExceptionMessage, nameof(propertyName));

            if (propertyValues.TryGetValue(propertyName, out var propertyValue))
            {
                if (propertyValue is PropertyValue<TProperty> genericValue)
                {
                    if (EqualityComparer<TProperty>.Default.Equals(genericValue.value, value)
                        || !OnPropertyChanging(new QueryPropertyChangingEventArgs<TProperty>(propertyName, value))) return false;

                    genericValue.value = value;
                }
                else
                {
                    if (EqualityComparer<TProperty>.Default.Equals(propertyValue.GetValue<TProperty>(), value)
                        || !OnPropertyChanging((QueryPropertyChangingEventArgs)Activator.CreateInstance(typeof(QueryPropertyChangingEventArgs<>).MakeGenericType(propertyValue.type), propertyName, value))) return false;

                    propertyValue.SetValue(value);
                }
            }
            else
            {
                var propertyType = GetPropertyType(propertyName);

                if (typeof(TProperty) == propertyType)
                {
                    if (!OnPropertyChanging(new QueryPropertyChangingEventArgs<TProperty>(propertyName, value))) return false;

                    propertyValue = new PropertyValue<TProperty>
                    {
                        type = propertyType,
                        value = value
                    };
                }
                else
                {
                    if (!OnPropertyChanging((QueryPropertyChangingEventArgs)Activator.CreateInstance(typeof(QueryPropertyChangingEventArgs<>).MakeGenericType(propertyType), propertyName, value))) return false;

                    propertyValue = (PropertyValue)Activator.CreateInstance(typeof(PropertyValue<>).MakeGenericType(propertyType));
                    propertyValue.type = propertyType;
                    propertyValue.SetValue(value);
                }

                propertyValues[propertyName] = propertyValue;
            }

            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            return true;
        }

        /// <summary>
        /// 속성 값 제거, 제거되면 다시 Get으로 속성 값을 가져올 때 속성 초기화 동작 결과나 기본 값을 반환함.
        /// </summary>
        /// <param name="propertyName">속성 명</param>
        /// <returns>제거 여부</returns>
        protected bool ClearProperty(string propertyName)
        {
            if (propertyValues.Remove(propertyName))
            {
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }


        private abstract class PropertyValue
        {
            public Type type;
            public abstract TValue GetValue<TValue>();
            public abstract void SetValue<TValue>(TValue value);
        }

        private class PropertyValue<T> : PropertyValue
        {
            public T value;

            public override TValue GetValue<TValue>()
            {
                if (value == null)
                    return default;
                
                return (TValue)Convert.ChangeType(value, typeof(TValue));
            }

            public override void SetValue<TValue>(TValue value)
            {
                if (Type.GetTypeCode(typeof(T)) == Type.GetTypeCode(typeof(TValue)))
                    this.value = (T)(object)value;
                else if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                    this.value = (T)Activator.CreateInstance(typeof(T), value);
                else
                    this.value = (T)Convert.ChangeType(value, typeof(T));
            }
        }
    }
}
