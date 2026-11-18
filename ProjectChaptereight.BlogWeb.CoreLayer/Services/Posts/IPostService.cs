using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Services.Posts
{
    public interface IPostService
    {
        OperationResult CreatePost(CreatePostDto command);
        OperationResult EditPost(EditPostDto command);
        PostDto GetPostById(int postId);


        //برای  استفاده در مطالب مرتبط این پروپرتی ایجاد گردید
        List<PostDto> GetRelatedPost(int groupId);

        //جهت ساخت محبوب ترین مقالات 
        List<PostDto> GetPopularPost();

        PostDto GetPostBySlug(string slug);
        PostFilterDto GetPostByFilter(PostFilterParams filterParams);

        //برای  مشاهده بازدید پست ها  
        void IncreaseVisit(int PostId);

        //bool IsSlugExist(string slug);
        bool IsSlugExist(string slug, int postId);
    }
}


