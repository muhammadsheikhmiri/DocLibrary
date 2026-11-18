using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Entities
{
    public class PostComment : BaseEntity<int>
    {

        [Required]
        public int UserID { get; set; }
        public int PostID { get; set; }
        [Required]
        public string Text { get; set; }





        #region Relations

        [ForeignKey("UserID")]
        public User user { get; set; }

        [ForeignKey("PostID")]
        public  Post Post { get; set; }
        #endregion

    }
}
