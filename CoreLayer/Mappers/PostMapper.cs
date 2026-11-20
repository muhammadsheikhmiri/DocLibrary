using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Mappers
{
    public static class PostMapper
    {
        public static Post MapperCreateDtoToPost(CreatePostDto dto) {
           return new Post()
            {
               Desceription = dto.Desceription,
               CategoryID = dto.CategoryID,
               Slug=dto.Slug,
               Title = dto.Title,
               UserID=dto.UserID,
               Visit=0,
               IsDeleted=false,
               SubCategoryID=dto.SubCategoryID,
               IsSpecial=dto.IsSpecial,
            };
        }
        public static PostDto MapToDto(Post post)
        {
            if (post == null)
                return null;
            return new PostDto()
            {
                Desceription = post.Desceription,
                CategoryID = post.CategoryID,
                Slug = post.Slug,
                Title = post.Title,
                UserFullName = post.User?.FullName,
                Visit = post.Visit,
                CreationDate = post.CreationDate,
                Category=post.Category==null?null: CategoryMapper.Map(post.Category),
                ImageName=post.ImageName,
                PdfName = post.PdfName,
                TxtName = post.TxtName,
                // این دو خط را اضافه کن
                WordName = post.WordName,
                ZipName = post.ZipName,
                PostId =post.ID,
                SubCategoryId=post.SubCategoryID,
                SubCategory = post.SubCategory==null?null: CategoryMapper.Map(post.Category),
                IsSpecial=post.IsSpecial,
            };
        }



        public static Post EditPost(EditPostDto ediDtio,Post post)
        {
            //این  همون  Post post  که از دیتابیس امده است
            {
                post.Desceription=ediDtio.Desceription;
                post.Title=ediDtio.Title;
                post.CategoryID=ediDtio.CategoryID;
                post.Slug=ediDtio.Slug;
                post.SubCategoryID=ediDtio.SubCategoryId;
                post.IsSpecial=ediDtio.IsSpecial;
                return post;
            }
        }

    }
}
