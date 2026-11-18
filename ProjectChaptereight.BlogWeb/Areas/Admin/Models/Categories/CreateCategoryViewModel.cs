using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Categories;
using System.ComponentModel.DataAnnotations;

namespace ProjectChaptereight.BlogWeb.Areas.Admin.Models.Categories
{
    public class CreateCategoryViewModel
    {


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن{0} اجباری است")]
        public string Title { get; set; }


        [Display(Name = "Slug")]
        [Required(ErrorMessage = "وارد کردن{0} اجباری است")]
        public string Slug { get; set; }

        [Display(Name = "MetaTag{با خط فاصله از یکدیگر جداکنید}")]
        public string MetaTag { get; set; }
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
        public int? ParentID { get; set; }
        public CreateCategoryDto MapToDto()
        {

            return new CreateCategoryDto
            {
                Title = Title,
                Slug = Slug,
                MetaTag = MetaTag,
                MetaDescription = MetaDescription,
                ParentID = ParentID
            };
        }
    }

}
