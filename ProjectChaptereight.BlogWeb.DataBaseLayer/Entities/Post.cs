using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Entities
{
    public class Post : BaseEntity<int>
    {
      
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public int? SubCategoryID { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }
        [Required]
        [MaxLength(400)]
        public string Slug { get; set; }
        [Required]
        [MaxLength(400)]
        public string Desceription { get; set; }
        public int Visit { get; set; }

        public  string?  ImageName { get; set; }

        //public string TxtFileName { get; set; }

        public string? PdfName { get; set; }

        public string? TxtName { get; set; }
        public string? WordName { get; set; }

        public string? ZipName { get; set; }
        //جهت استفاده در  پست ویژه
        public bool IsSpecial {get;set;}


        //جهت ثب کلمه ی کلیدی در متا تگ
        //public string KeyWord  { get; set; }



        #region Relations
        //این ارتباط  یک به چند است
        [ForeignKey("UserID")]
        public User User { get; set; }

        //این ارتباط  یک به چند است
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        //این ارتباط  یک به چند است
        [ForeignKey("SubCategoryID")]
        public Category SubCategory { get; set; }

        public ICollection<PostComment> PostComments { get; set; }= new List<PostComment>();
        #endregion
    }
}
