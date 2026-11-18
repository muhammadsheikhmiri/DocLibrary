using Microsoft.AspNetCore.Mvc;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.FileManager;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;

namespace ProjectChaptereight.BlogWeb.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        private readonly IFileManager _fileManager;
            public UploadController(IFileManager fileManager)
        {
            _fileManager= fileManager;
        }
        [Route("/Upload/Article")]
      
        public IActionResult UploadArticleImage(IFormFile upload)
        {
            if (upload == null)
                BadRequest();
            var imageName = _fileManager.SaveFileAndReturnName(upload, Directories.PostContentImage);
            //دریافت کامل ادرس  توسط ckeditor
            return  Json(new {Uploaded=true,url=Directories.GetContentImage(imageName) });

        }
    }
}
