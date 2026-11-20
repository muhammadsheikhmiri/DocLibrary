using Microsoft.AspNetCore.Http;

namespace ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts
{
    public class EditPostDto
    {
       public int PostId { get; set; }
        public int CategoryID { get; set; }
        public int? SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Desceription { get; set; }
        public bool IsSpecial { get; set; }
        //public IFormFile? ImageFile { get; set; }
        /// <summary>
        /// لیست فایل‌های جدید انتخاب شده برای ویرایش پست
        /// </summary>
        public List<IFormFile>? Files { get; set; }

    }
}
