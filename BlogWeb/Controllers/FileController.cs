using Microsoft.AspNetCore.Mvc;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;

namespace ProjectChaptereight.BlogWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public FileController(IWebHostEnvironment hostEnvironment)
        {
            environment = hostEnvironment;
        }

        [HttpGet("download")]
        public IActionResult Download(string fileName, string fileType)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return NotFound();

            // جلوگیری از ارسال مسیر به‌جای نام فایل
            if (fileName != Path.GetFileName(fileName))
                return BadRequest();

            string folderPath;
            string contentType;

            switch (fileType?.ToLowerInvariant())
            {
                case "image":
                    folderPath = Directories.PostImage;

                    var imageExtension = Path.GetExtension(fileName)
                        .ToLowerInvariant();

                    contentType = imageExtension switch
                    {
                        ".jpg" => "image/jpeg",
                        ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".webp" => "image/webp",
                        _ => "application/octet-stream"
                    };

                    break;

                case "pdf":
                    folderPath = Directories.PostPdf;
                    contentType = "application/pdf";
                    break;

                case "txt":
                    folderPath = Directories.PostTxt;
                    contentType = "text/plain";
                    break;

                case "word":
                    folderPath = Directories.PostWord;

                    var wordExtension = Path.GetExtension(fileName)
                        .ToLowerInvariant();

                    if (wordExtension != ".doc" &&
                        wordExtension != ".docx")
                    {
                        return NotFound();
                    }

                    contentType = wordExtension == ".docx"
                        ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                        : "application/msword";

                    break;

                case "zip":
                    folderPath = Directories.PostZip;

                    if (Path.GetExtension(fileName).ToLowerInvariant() != ".zip")
                        return NotFound();

                    contentType = "application/zip";
                    break;

                default:
                    return NotFound();
            }

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                folderPath,
                fileName
            );

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(
                fileBytes,
                contentType,
                Path.GetFileName(filePath)
            );
        }
    }
}