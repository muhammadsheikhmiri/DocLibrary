using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Comments;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Comments;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Posts;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using System.ComponentModel.DataAnnotations;

namespace ProjectChaptereight.BlogWeb.Pages
{
    public class PostModel : PageModel
    {
      
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }
        public PostDto Post { get; set; }
        [Required]
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]
        public int PostId { get; set; }


        public List<CommentDto> comments { get; set; }
        public List<PostDto> RelatedPost { get; set; }
        public IActionResult OnGet(string slug)
        {
            Post = _postService.GetPostBySlug(slug);
            if (Post == null)
                return NotFound();
            //لود کردن کامنت  ها 
            comments = _commentService.GetPostCooments(Post.PostId);
            RelatedPost = _postService.GetRelatedPost(Post.SubCategoryId ?? Post.CategoryID);
            _postService.IncreaseVisit(Post.PostId);
            return Page();
        }
        public IActionResult OnPost(string slug) {

            //اگر کاربرلاگین نکرده بود
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("Post", new { slug });
            if (!ModelState.IsValid)
            {

                Post = _postService.GetPostBySlug(slug);
                comments = _commentService.GetPostCooments(Post.PostId);
                RelatedPost = _postService.GetRelatedPost(Post.SubCategoryId ?? Post.CategoryID);
                return Page();
            }

            _commentService.CreateComment(new CoreLayer.DTOs.Comments.CreateCommentDto()
            {
                PostID=PostId,
                Text=Text,
                UserID=User.GetUserId()
            });
            return RedirectToPage("Post", new { slug });
        }
    }
}
