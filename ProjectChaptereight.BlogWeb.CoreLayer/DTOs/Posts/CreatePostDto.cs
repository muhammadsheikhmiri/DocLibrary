using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts
{
    public class CreatePostDto
    {
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public int? SubCategoryID { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Desceription { get; set; }
        public bool IsSpecial { get; set; }

        //IformFile:برای  ذخیره ی  فایل  هست و باید  تعریف شود(image)
        //public IFormFile ImageFile { get; set; }
        ////برای دریافت فایل های با  فرمت  txt 
        //[Display(Name = "فایل متنی")]
        //public IFormFile TxtFile { get; set; }
        /// <summary>
        /// لیست فایل‌های انتخاب شده برای پست
        /// </summary>
        public List<IFormFile>? Files { get; set; }
    }
}
