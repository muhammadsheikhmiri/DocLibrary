using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Comments;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Services.Comments
{
    public interface ICommentService
    {
        OperationResult CreateComment(CreateCommentDto command);
        List<CommentDto> GetPostCooments(int postId);

    }
}
