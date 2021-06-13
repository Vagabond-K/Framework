using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK.App
{
    /// <summary>
    /// 기본 페이지 데이터
    /// </summary>
    public class PageData : IPageData
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewModelTypeName">페이지 뷰 모델 형식 이름</param>
        /// <param name="viewTypeName">페이지 뷰 형식 이름</param>
        public PageData(string viewModelTypeName, string viewTypeName)
        {
            ViewModelTypeName = viewModelTypeName;
            ViewTypeName = viewTypeName;
        }

        private string title;

        /// <summary>
        /// 속성 값이 변경될 때 발생합니다.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 페이지 제목
        /// </summary>
        public string Title { get => title; set => this.Set(ref title, value, PropertyChanged); }

        /// <summary>
        /// 페이지 뷰 모델 형식 이름
        /// </summary>
        public string ViewModelTypeName { get; }

        /// <summary>
        /// 페이지 뷰 형식 이름
        /// </summary>
        public string ViewTypeName { get; }
    }
}
