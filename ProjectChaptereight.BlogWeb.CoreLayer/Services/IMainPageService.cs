using Microsoft.EntityFrameworkCore;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs;
using ProjectChaptereight.BlogWeb.CoreLayer.Mappers;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Posts;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Context;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Services
{
    public interface IMainPageService
    {
        MainPageDto GetDate();

    }
    public class MainPageService : IMainPageService
    {
        private readonly BolgContext _context;
        private readonly IPostService _postService;

        public MainPageService(BolgContext context, IPostService postService)
        {
            _context= context;
            _postService= postService;
        }

        public MainPageDto GetDate()
        {
            var categories = _context.categories.OrderByDescending(d => d.ID).Take(6)
                 .Include(c => c.Posts)
                 .Include(c => c.Posts)
                 .Include(c => c.SubPosts)
                 .Select(category => new MainPageCategoryDto()
                 {
                     Title = category.Title,
                     Slug = category.Slug,
                     PostChild = category.Posts.Count + category.SubPosts.Count,
                     IsMainCategory = category.ParentID == null
                 }).ToList();
            //گرفتن لیست مقالات ویژه 
            var specialPosts = _context.posts
                .OrderByDescending(d => d.ID)
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .Where(r => r.IsSpecial).Take(4).Select(Posts => PostMapper.MapToDto(Posts)).ToList();

            var latestPos = _context.posts
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .OrderByDescending(d => d.ID)
                .Take(6).Select(post => PostMapper.MapToDto(post)).ToList();

            //در آخر کل مدل را ریترن می کنیم 
            return new MainPageDto()
            {
                LatesPosts = latestPos,
                Categories = categories,
                SpecialPosts = specialPosts

            };

        }
    }
}
