using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Utilities
{
    public class ImageValidarion
    {
        public static Boolean Validate(string imageName)
        {
            //دریافت فرمت فایل ضمیمه شده
            var ex=Path.GetExtension(imageName);
            if(ex==null)
                return false;
            if(ex.ToLower()==".Png"||ex.ToLower()==".jpg")
                return true;
            return false;

        }

        public static Boolean Validate(IFormFile file)
        {
            try
            {
                using var image = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }catch
            {
                return false;
            }
        }
    }
}
