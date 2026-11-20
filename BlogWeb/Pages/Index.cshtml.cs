using Azure.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts;
using ProjectChaptereight.BlogWeb.CoreLayer.Services;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Categories;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Posts;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;
using Azure.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  ProjectChaptereight.BlogWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly IMainPageService _mainPageService;
        public IndexModel(IPostService postService, IMainPageService mainPageService)
        {
            _postService = postService;
            _mainPageService = mainPageService;
        }

        public MainPageDto MainPageData { get; set; }

        public void OnGet()
        {
            MainPageData = _mainPageService.GetDate();
        }

        public IActionResult OnGetLatestPosts(string categorySlug)
        {
            var filterDto = _postService.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                
                PageID = 1,
                Take = 6
            });
            return Partial("_LatestPosts", filterDto.Posts);
        }
        public IActionResult OnGetPopularPost()
        {
            return Partial("_PopularPosts", _postService.GetPopularPost());
        }

    }
}