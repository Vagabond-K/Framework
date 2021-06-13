using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK.App
{
    /// <summary>
    /// 페이지 데이터 인터페이스
    /// </summary>
    public interface IPageData : INotifyPropertyChanged
    {
        /// <summary>
        /// 페이지 제목
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// 페이지 뷰 모델 형식 이름
        /// </summary>
        string ViewModelTypeName { get; }

        /// <summary>
        /// 페이지 뷰 형식 이름
        /// </summary>
        string ViewTypeName { get; }
    }
}
