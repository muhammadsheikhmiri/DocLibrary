using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace ProjectChaptereight.BlogWeb.Areas.Admin.Models.Posts
{
    public class CreatePostViewModel
    {


        [Display(Name ="انتخاب دسته بندی")]
        [Required(ErrorMessage ="لطفا {0}را وارد نمایید")]
        public int CategoryID { get; set; }

        [Display(Name = "انتخاب زیر دسته")]
        [Required(ErrorMessage = "لطفا {0}را وارد نمایید")]
        public int? SubCategoryID { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0}را وارد نمایید")]
        public string Title { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "لطفا {0}را وارد نمایید")]
        public string Slug { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0}را وارد نمایید")]
        [UIHint("CkEditor4")]
        public string Desceription { get; set; }

        [Display(Name ="پست ویژه ؟")]
        public bool Special { get; set; }

        //[Display(Name = "عکس")]
        //[Required(ErrorMessage = "لطفا {0}را وارد نمایید")]

        ////IformFile:برای  ذخیره ی  فایل  هست و باید  تعریف شود(image)
        //public IFormFile ImageFile { get; set; }
        ////برای دریافت فایل های با  فرمت  txt 
        ////[Display(Name = "فایل متنی")]
        ////public IFormFile TxtFile { get; set; }

        /// <summary>
        /// فایل‌های انتخاب شده برای پست
        /// </summary>
        [Display(Name = "فایل‌ها")]
        public List<IFormFile>? Files { get; set; }
    }
}
