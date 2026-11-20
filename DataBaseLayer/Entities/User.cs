using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Entities
{
    public class User:BaseEntity<int>
    {


        [Required]
        public string FullName { get; set; }


        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        public string Email { get; set; }
        [Required]
        public string  Pasword { get; set; }

        public UserRole Role { get; set; }



        //برای حذف یک ردیف در دیتابیس استفاده میشود 
        public bool IsDeleted { get; set; }

        #region Relations
        public ICollection<Post> posts { get; set; }
        //جلوگیری از خطای NullReference 
        public ICollection<PostComment> PostComment { get; set; } = new List<PostComment>();
        #endregion
    }
    //Enum=ConstantName
    public enum UserRole
    {
        Admin,
        User,
        Writer

    }

}
