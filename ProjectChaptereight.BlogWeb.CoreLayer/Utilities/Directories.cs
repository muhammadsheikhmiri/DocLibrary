using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Utilities
{
    public class Directories
    {
        // مسیر عکس پست
        public const string PostImage = "wwwroot/images/posts";

        // مسیر عکس‌های داخل محتوای پست
        public const string PostContentImage = "wwwroot/images/posts/content";

        // مسیر فایل PDF پست
        public const string PostPdf = "wwwroot/files/posts/pdf";

        // مسیر فایل TXT پست
        public const string PostTxt = "wwwroot/files/posts/txt";

        // مسیر فایل Word پست
        public const string PostWord = "wwwroot/files/posts/word";

        // مسیر فایل ZIP پست
        public const string PostZip = "wwwroot/files/posts/zip";


        // مسیر قابل استفاده در View برای عکس پست
        public static string GetPostImage(string imageName)
            => $"{PostImage.Replace("wwwroot", "")}/{imageName}";


        // مسیر قابل استفاده در View برای عکس داخل محتوا
        public static string GetContentImage(string imageName)
            => $"{PostContentImage.Replace("wwwroot", "")}/{imageName}";


        // مسیر قابل استفاده در View برای PDF
        public static string GetPostPdf(string pdfName)
            => $"{PostPdf.Replace("wwwroot", "")}/{pdfName}";


        // مسیر قابل استفاده در View برای TXT
        public static string GetPostTxt(string txtName)
            => $"{PostTxt.Replace("wwwroot", "")}/{txtName}";


        // مسیر قابل استفاده در View برای Word
        public static string GetPostWord(string wordName)
            => $"{PostWord.Replace("wwwroot", "")}/{wordName}";


        // مسیر قابل استفاده در View برای ZIP
        public static string GetPostZip(string zipName)
            => $"{PostZip.Replace("wwwroot", "")}/{zipName}";
    }
}