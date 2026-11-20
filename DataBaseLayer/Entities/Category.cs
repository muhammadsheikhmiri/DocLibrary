using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Entities
{
    public class Category : BaseEntity<int>
    {

        public string Title { get; set; }
     
        public string Slug { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }

        //برای  ساخت زیر مجموع های  درخت واره ای
        public  int? ParentID { get; set; }

        #region Relations
        //InverseProperty:مچ کردن دوتا فیلد
        [InverseProperty("Category")]
        //جلوگیری از خطای NullReference 
        public ICollection<Post> Posts { get; set; }= new List<Post>();
        [InverseProperty("SubCategory")]
        public ICollection<Post> SubPosts { get; set; } = new List<Post>();
        #endregion


    }
}
