using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Categories;

namespace ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string UserFullName { get; set; }
        public int CategoryID { get; set; }
        public int? SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Desceription { get; set; }
        //نام  عکسی که درسرور  ذخیره شد
        public string ImageName { get; set; }
        // نام فایل PDF ذخیره شده در سرور
        public string PdfName { get; set; }
        public string? WordName { get; set; }

        public string? ZipName { get; set; }
        // نام فایل TXT ذخیره شده در سرور
        public string TxtName { get; set; }

        //برای پست ویژه
        public bool IsSpecial { get; set; }

        public DateTime CreationDate { get; set; }

        public int Visit { get; set; }

        //جهت  ارسال به  category
        public CategoryDto Category { get; set; }

        public CategoryDto SubCategory { get; set; }
    }
}
