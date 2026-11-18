using Microsoft.AspNetCore.Http;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Services.FileManager
{
    public class FileManager : IFileManager
    {
        //کارذخیره سازی عمومی فایل رو انجام میده 
        public string SaveFileAndReturnName(IFormFile file, string savaPath)
        {
            if (file == null) throw new Exception("هیچ فایلی جهت بارگذاری وجودندارد");

            //نام فایل حتما باید یونیک باشد
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), savaPath.Replace("/","\\"));
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            //مسیر کامل  فایل  هست
            var fullpath=Path.Combine(folderpath,fileName);
            using var stream = new FileStream(fullpath, FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }
        public string SaveWordFileAndReturnName(IFormFile file, string savaPath)
        {
            if (file == null)
                throw new Exception("هیچ فایلی جهت بارگذاری وجود ندارد");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (extension != ".doc" && extension != ".docx")
                throw new Exception("فقط فایل Word مجاز است");

            return SaveFileAndReturnName(file, savaPath);
        }

       //ذخیره ی  فایل زیپ
       public  string SaveZipFileAndReturnName(IFormFile file,string savePath)
        {

            if (file == null)
            {
                throw new Exception("هیچ فایلی جهت بارگذاری وجود ندارد");
            }
            //پسوند فایل را دریافت میکند 
            //Path.GetExtension:خروجی را با نقطه برمیگرداند
            var extensions = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extensions != ".zip")
                throw new Exception("فقط فایل با فرمت zip مجاز میباشد");
            if (file.Length > 250L * 1024 * 1024)
                throw new Exception("حجم فایل ZIP نباید بیشتر از 250 مگابایت باشد");
            return SaveFileAndReturnName(file,savePath);
        }


        void IFileManager.DeleteFile(string filename, string path)
        {
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), path, filename);
            if(File.Exists(folderpath))
                File.Delete(folderpath);
        }
        public string SaveImageAndReturnImageName(IFormFile file, string savaPath)
        {
            var isNotImage = !ImageValidarion.Validate(file);
            if (isNotImage)
                throw new Exception();
            return SaveFileAndReturnName(file, savaPath);
        }
        //جهت بارگذاری  فایل txt 
        public string SaveTextFileAndReturnName(IFormFile file, string savaPath)
        {
            if (file == null)
                throw new Exception("هیچ فایلی جهت بارگذاری وجود ندارد");

            if (Path.GetExtension(file.FileName).ToLower() != ".txt")
                throw new Exception("فقط فایل TXT مجاز است");

            return SaveFileAndReturnName(file, savaPath);
        }
        public string SavePdfFileAndReturnName(IFormFile file, string savaPath)
        {
            if (file == null)
                throw new Exception("هیچ فایلی جهت بارگذاری وجود ندارد");

            if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
                throw new Exception("فقط فایل PDF مجاز است");

            return SaveFileAndReturnName(file, savaPath);
        }
    }
}
