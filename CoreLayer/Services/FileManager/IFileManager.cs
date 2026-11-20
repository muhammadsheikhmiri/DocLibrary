using Microsoft.AspNetCore.Http;

public interface IFileManager
{
    /// <summary>
    /// فایل را در مسیر مشخص شده ذخیره کرده و نام فایل ذخیره شده را برمی‌گرداند.
    /// </summary>
    string SaveFileAndReturnName(IFormFile file, string savaPath);


    /// <summary>
    /// فایل مشخص شده را از مسیر مورد نظر حذف می‌کند.
    /// </summary>
    void DeleteFile(string filename, string path);


    /// <summary>
    /// فایل تصویر را اعتبارسنجی کرده، در مسیر مشخص ذخیره کرده
    /// و نام فایل ذخیره شده را برمی‌گرداند.
    /// </summary>
    string SaveImageAndReturnImageName(IFormFile file, string savaPath);


    /// <summary>
    /// فایل متنی TXT را اعتبارسنجی کرده، در مسیر مشخص ذخیره کرده
    /// و نام فایل ذخیره شده را برمی‌گرداند.
    /// </summary>
    string SaveTextFileAndReturnName(IFormFile file, string savaPath);


    /// <summary>
    /// فایل PDF را اعتبارسنجی کرده، در مسیر مشخص ذخیره کرده
    /// و نام فایل ذخیره شده را برمی‌گرداند.
    /// </summary>
    string SavePdfFileAndReturnName(IFormFile file, string savaPath);


    string SaveWordFileAndReturnName(IFormFile file, string savaPath);

    string SaveZipFileAndReturnName(IFormFile file, string savaPath);
}