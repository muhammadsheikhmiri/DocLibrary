using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts
{
    public class PostFilterDto: BasePagination
    {
   
        public List<PostDto> Posts { get; set; }
    public PostFilterParams FilterParams { get; set; }
    }

    public class PostFilterParams
    {
        //Take=تعداد پست هایی  است که می خواهیم که در ک صفحه نمایش بدهد
        public int Take { get; set; }
        public int PageID { get; set; }
        public string Title { get; set; }
        public string CategorySlug { get; set; }
    }
}
