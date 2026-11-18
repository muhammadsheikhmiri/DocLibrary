using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectChaptereight.BlogWeb.Areas.Admin.Models.Posts;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Posts;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;

namespace ProjectChaptereight.BlogWeb.Areas.Admin.Controllers
{
    //[Area("Admin")]
    //[A 
    public class PostController : AdminControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int pageId=1,string title="",string categorySlug="")
        {

      
            var param = new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageID = pageId,
                Title = title,
                Take = 1,

            };
            var model = _postService.GetPostByFilter(param);
            return View(model);
        }
        public IActionResult Add()
        {
        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add( CreatePostViewModel createPostViewModel)
        {


            if (!ModelState.IsValid)
            {

                return View();
            }

            var result = _postService.CreatePost(new CreatePostDto()
            {
                CategoryID = createPostViewModel.CategoryID,
                Title = createPostViewModel.Title,
                Files = createPostViewModel.Files,
                Slug = createPostViewModel.Slug,
                SubCategoryID = createPostViewModel.SubCategoryID == 0
           ? null
           : createPostViewModel.SubCategoryID,
                Desceription = createPostViewModel.Desceription,
                IsSpecial = createPostViewModel.Special,
                UserID = User.GetUserId(),
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(createPostViewModel);
            }

            //return RedirectToAction("Index");
            return RedirectAndShowAlert(result, RedirectToAction("Index"));

        }
        public IActionResult Edit(int id)
        {
            var post=_postService.GetPostById(id);
            if (post == null)
                return RedirectToAction("Index");

            //var model = new EditPostViewModel()
            //{
            //    CategoryID = post.CategoryID,
            //    Desceription = post.Desceription,
            //    Slug = post.Slug,
            //    SubCategoryID = post.SubCategoryId,
            //    Title = post.Title,
            //    Special=post.IsSpecial

            //};
            var model = new EditPostViewModel()
            {
                CategoryID = post.CategoryID,
                Desceription = post.Desceription,
                Slug = post.Slug,
                SubCategoryID = post.SubCategoryId,
                Title = post.Title,
                Special = post.IsSpecial,
                ImageName = post.ImageName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,EditPostViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editViewModel);
            }
            var result = _postService.EditPost(new EditPostDto()
            {
                CategoryID = editViewModel.CategoryID,
                Title = editViewModel.Title,
                Files = editViewModel.Files,
                Slug = editViewModel.Slug,
                SubCategoryId = editViewModel.SubCategoryID == 0
          ? null
          : editViewModel.SubCategoryID,
                Desceription = editViewModel.Desceription,
                PostId = id,
                IsSpecial = editViewModel.Special,
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(editViewModel);
            }
            //return RedirectToAction("Index");
            return RedirectAndShowAlert(result, RedirectToAction("Index"));

        }

    }
}
