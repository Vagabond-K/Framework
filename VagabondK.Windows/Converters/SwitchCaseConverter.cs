using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace VagabondK.Windows.Converters
{
    /// <summary>
    /// switch case문과 같은 동작의 결과로 변환
    /// </summary>
    [ContentProperty(nameof(Map))]
    public sealed class SwitchCaseConverter : IValueConverter, IMultiValueConverter
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public SwitchCaseConverter()
        {
            Map = new ObservableCollection<SwitchCaseConverterItem>();
        }

        private ObservableCollection<SwitchCaseConverterItem> map;
        private Dictionary<object, SwitchCaseConverterItem> dictionary;

        /// <summary>
        /// 각 case별 맵
        /// </summary>
        public ObservableCollection<SwitchCaseConverterItem> Map
        {
            get => map;
            set
            {
                if (map != value)
                {
                    if (map != null)
                        map.CollectionChanged -= OnMapCollectionChanged;
                    map = value;
                    if (map != null)
                        map.CollectionChanged += OnMapCollectionChanged;
                    dictionary = null;
                }
            }
        }

        private void OnMapCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            dictionary = null;
        }

        private Dictionary<object, SwitchCaseConverterItem> Dictionary
        {
            get
            {
                if (dictionary == null)
                    dictionary = map.ToDictionary(item => item.Case, item => item);

                return dictionary;
            }
        }

        /// <summary>
        /// 값을 변환합니다.
        /// </summary>
        /// <param name="value">바인딩 소스에서 생성한 값입니다.</param>
        /// <param name="targetType">바인딩 대상 속성의 형식입니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>변환된 값입니다.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Dictionary.TryGetValue(value, out var result) ? result?.Value : DependencyProperty.UnsetValue;

        /// <summary>
        /// 값을 변환합니다.
        /// </summary>
        /// <param name="value">바인딩 대상에서 생성한 값입니다.</param>
        /// <param name="targetType">변환할 대상 형식입니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>변환된 값입니다.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 소스 값을 바인딩 대상의 값으로 변환합니다. 데이터 바인딩 엔진이 소스 바인딩에서 바인딩 대상으로 값을 전파할 때 이 메서드를 호출합니다.
        /// </summary>
        /// <param name="values">System.Windows.Data.MultiBinding의 소스 바인딩에서 생성하는 값의 배열입니다.
        /// System.Windows.DependencyProperty.UnsetValue 값은 변환에 제공할 값이 소스 바인딩에 없음을 나타냅니다.</param>
        /// <param name="targetType">바인딩 대상 속성의 형식입니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>변환된 값입니다.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value != null)
                    {
                        if (Dictionary.TryGetValue(value, out var result))
                        {
                            return result?.Value;
                        }
                    }
                }
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// 바인딩 대상 값을 소스 바인딩 값으로 변환합니다.
        /// </summary>
        /// <param name="value">바인딩 대상에서 생성하는 값입니다.</param>
        /// <param name="targetTypes">변환할 형식의 배열입니다. 배열 길이는 메서드에서 반환하도록 제안되는 값의 개수와 형식을 나타냅니다.</param>
        /// <param name="parameter">사용할 변환기 매개 변수입니다.</param>
        /// <param name="culture">변환기에서 사용할 문화권입니다.</param>
        /// <returns>대상 값에서 소스 값으로 다시 변환된 값의 배열입니다.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// SwitchCaseConverter의 Map 요소
    /// </summary>
    public class SwitchCaseConverterItem
    {
        /// <summary>
        /// case
        /// </summary>
        public object Case { get; set; }

        /// <summary>
        /// case에 해당하는 값
        /// </summary>
        public object Value { get; set; }
    }
}
