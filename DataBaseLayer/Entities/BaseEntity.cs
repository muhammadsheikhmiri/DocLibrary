using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Entities
{
    //Tkey:T(type)
    //key:کلید اصلی
    public class BaseEntity<Tkey>
    {
        [Key]
        public Tkey ID { get; set; }


        public DateTime CreationDate { get; set; } = DateTime.Now;

        //برای حذف یک ردیف در دیتابیس استفاده میشود 
        public bool IsDeleted { get; set; }
    }
}
