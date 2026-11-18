using System.ComponentModel.DataAnnotations;

namespace ProjectChaptereight.BlogWeb.Areas.Admin.Models.Posts
{
    public class EditPostViewModel
    {

        [Display(Name = "انتخاب دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int CategoryID { get; set; }


        [Display(Name = "انتخاب زیر دسته")]
        public int? SubCategoryID { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Title { get; set; }


        [Display(Name = "Slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Slug { get; set; }


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [UIHint("CkEditor4")]
        public string Desceription { get; set; }


        [Display(Name = "پست ویژه ؟")]
        public bool Special { get; set; }


        //[Display(Name = "عکس")]
        //public IFormFile? ImageFile { get; set; }


        //// عکس فعلی مقاله
        //public string? ImageName { get; set; }
        /// <summary>
        /// فایل‌های جدید انتخاب شده برای ویرایش پست
        /// </summary>
        [Display(Name = "فایل‌ها")]
        public List<IFormFile>? Files { get; set; }

        public string? ImageName { get; set; }
    }
}