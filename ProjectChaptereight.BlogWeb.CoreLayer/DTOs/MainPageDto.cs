using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.DTOs
{

    public class MainPageDto
    {
        public List<PostDto> LatesPosts { get; set; }
        public List<PostDto> SpecialPosts { get; set; }
        public List<MainPageCategoryDto> Categories { get; set; }
    }

    public class MainPageCategoryDto
    {
        //MainCategory را در بالای صفحه نمایش  میدهد
        public bool IsMainCategory { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
       public int PostChild { get; set; }
    }

}
