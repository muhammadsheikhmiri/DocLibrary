using Microsoft.EntityFrameworkCore;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Comments;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Context;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Services.Comments
{
    public class CommentService : ICommentService
    {

        private readonly BolgContext _context;
        private readonly ICommentService _commentService;


        public CommentService( BolgContext context)
        {
          
            _context = context;
        }
        OperationResult ICommentService.CreateComment(CreateCommentDto command)
        {
            var comment = new PostComment()
            {
                PostID = command.PostID,
                Text= command.Text,
                UserID = command.UserID,
            };
           _context.postComments.Add(comment);
          _context.SaveChanges();
            return OperationResult.Success();
        }

         List<CommentDto> ICommentService.GetPostCooments(int postId)
        {
            return _context.postComments.Include(c=>c.user).Where(c => c.PostID == postId).Select(s => new  CommentDto{
                PostID = s.PostID,
                Text= s.Text,
                UserFullName=s.user.FullName,    
                CommentId=s.ID,
                CreationDate= s.CreationDate,
            }).ToList();
        }
    }
}
