using Microsoft.EntityFrameworkCore;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Posts;
using ProjectChaptereight.BlogWeb.CoreLayer.Mappers;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.FileManager;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Context;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly BolgContext _context;
        private readonly IFileManager _fileManager;

        public PostService(BolgContext context, IFileManager fileManager)
        {
            _context=context;
            _fileManager = fileManager;
        }

        public OperationResult CreatePost(CreatePostDto command)
        {
            var post = PostMapper.MapperCreateDtoToPost(command);

            if (command.Files != null && command.Files.Any())
            {
                int imageCount = 0;
                int pdfCount = 0;
                int txtCount = 0;
                int wordCount = 0;
                int zipCount = 0;

                // مرحله اول: اعتبارسنجی فایل‌ها
                foreach (var file in command.Files)
                {
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".webp":
                            imageCount++;
                            break;

                        case ".pdf":
                            pdfCount++;
                            break;

                        case ".txt":
                            txtCount++;
                            break;

                        case ".doc":
                        case ".docx":
                            wordCount++;
                            break;

                        case ".zip":
                            zipCount++;
                            break;

                        default:
                            return OperationResult.Error(
                                $"فرمت فایل {Path.GetFileName(file.FileName)} مجاز نیست");
                    }

                    // بررسی حجم فایل ZIP
                    if (extension == ".zip" &&
                        file.Length > 250L * 1024 * 1024)
                    {
                        return OperationResult.Error(
                            "حجم فایل ZIP نباید بیشتر از 250 مگابایت باشد");
                    }
                }

                // مرحله دوم: بررسی تعداد فایل‌ها
                if (imageCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک تصویر مجاز است");

                if (pdfCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک فایل PDF مجاز است");

                if (txtCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک فایل TXT مجاز است");

                if (wordCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک فایل Word مجاز است");

                if (zipCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک فایل ZIP مجاز است");

                // مرحله سوم: ذخیره فایل‌ها
                foreach (var file in command.Files)
                {
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".webp":

                            post.ImageName = _fileManager.SaveImageAndReturnImageName(
                                file,
                                Directories.PostImage);

                            break;

                        case ".pdf":

                            post.PdfName = _fileManager.SavePdfFileAndReturnName(
                                file,
                                Directories.PostPdf);

                            break;

                        case ".txt":

                            post.TxtName = _fileManager.SaveTextFileAndReturnName(
                                file,
                                Directories.PostTxt);

                            break;

                        case ".doc":
                        case ".docx":

                            post.WordName = _fileManager.SaveWordFileAndReturnName(
                                file,
                                Directories.PostWord);

                            break;

                        case ".zip":

                            post.ZipName = _fileManager.SaveZipFileAndReturnName(
                                file,
                                Directories.PostZip);

                            break;
                    }
                }
            }

            _context.posts.Add(post);
            _context.SaveChanges();

            return OperationResult.Success();
        }
        //public OperationResult EditPost(EditPostDto command)
        //{
        //    var Post = _context.posts.FirstOrDefault(p => p.ID == command.PostId);
        //    //نام عکس قبلی را در اینجا ذخیره می کینم
        //    var oldImage = Post.ImageName;
        //    if (Post == null)
        //        return OperationResult.NotFound();
        //    //Post = PostMapper.MapperEditDtoToPost(command, Post);
        //    //_context.posts.Update(Post);
        //    PostMapper.EditPost(command, Post);
        //    //اگر پر بود باید  فایل را  ذخیره کنیم
        //    if (command.ImageFile != null)
        //    {
        //        Post.ImageName = _fileManager.SaveImageAndReturnImageName(command.ImageFile, Directories.PostImage);

        //    }
        //    if(command.Slug.ToSlug()!=Post.Slug)
        //        if(IsSlugExist(command.Slug))
        //            return OperationResult.Error("این اسلاگ از  قبل وجود دارد");
        //    _context.SaveChanges();
        //    //if (command.ImageFile != null):از  این  خط فقط زمانی استفاده می کنیم که نیاز باشد   بعد از درج  عکس جدید  عکس  قبلی حذف شود
        //    //یعنی فایل جدید  وارد  کرده باشد 
        //    if (command.ImageFile != null)
        //    {
        //        //نام  قبلی image را  در  اینجا نیاز  داریم
        //        _fileManager.DeleteFile(oldImage, Directories.PostImage);
        //    }
        //        return OperationResult.Success();
        //}
        public OperationResult EditPost(EditPostDto command)
        {
            var Post = _context.posts
                .FirstOrDefault(p => p.ID == command.PostId);

            if (Post == null)
                return OperationResult.NotFound();

            // بررسی تکراری نبودن Slug
            if (IsSlugExist(command.Slug, command.PostId))
            {
                return OperationResult.Error("این اسلاگ از قبل وجود دارد");
            }

            // ذخیره نام فایل‌های قبلی
            var oldImage = Post.ImageName;
            var oldPdf = Post.PdfName;
            var oldTxt = Post.TxtName;
            var oldWord = Post.WordName;
            var oldZip = Post.ZipName;

            // آپدیت اطلاعات مقاله
            PostMapper.EditPost(command, Post);

            if (command.Files != null && command.Files.Any())
            {
                int imageCount = 0;
                int pdfCount = 0;
                int txtCount = 0;
                int wordCount = 0;
                int zipCount = 0;

                // مرحله اول: بررسی فرمت، تعداد و حجم فایل‌ها
                foreach (var file in command.Files)
                {
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".webp":
                            imageCount++;
                            break;

                        case ".pdf":
                            pdfCount++;
                            break;

                        case ".txt":
                            txtCount++;
                            break;

                        case ".doc":
                        case ".docx":
                            wordCount++;
                            break;

                        case ".zip":
                            zipCount++;

                            if (file.Length > 250L * 1024 * 1024)
                            {
                                return OperationResult.Error(
                                    "حجم فایل ZIP نباید بیشتر از 250 مگابایت باشد");
                            }

                            break;

                        default:
                            return OperationResult.Error(
                                $"فرمت فایل {Path.GetFileName(file.FileName)} مجاز نیست");
                    }
                }

                // بررسی تعداد فایل‌ها
                if (imageCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک تصویر مجاز است");

                if (pdfCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک فایل PDF مجاز است");

                if (txtCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک فایل TXT مجاز است");

                if (wordCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک فایل Word مجاز است");

                if (zipCount > 1)
                    return OperationResult.Error("برای هر پست فقط یک فایل ZIP مجاز است");

                // مرحله دوم: ذخیره فایل‌های جدید
                foreach (var file in command.Files)
                {
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".webp":

                            Post.ImageName = _fileManager.SaveImageAndReturnImageName(
                                file,
                                Directories.PostImage);

                            break;

                        case ".pdf":

                            Post.PdfName = _fileManager.SavePdfFileAndReturnName(
                                file,
                                Directories.PostPdf);

                            break;

                        case ".txt":

                            Post.TxtName = _fileManager.SaveTextFileAndReturnName(
                                file,
                                Directories.PostTxt);

                            break;

                        case ".doc":
                        case ".docx":

                            Post.WordName = _fileManager.SaveWordFileAndReturnName(
                                file,
                                Directories.PostWord);

                            break;

                        case ".zip":

                            Post.ZipName = _fileManager.SaveZipFileAndReturnName(
                                file,
                                Directories.PostZip);

                            break;
                    }
                }
            }

            _context.SaveChanges();

            // مرحله سوم: حذف فایل‌های قبلی که جایگزین شده‌اند
            if (command.Files != null && command.Files.Any())
            {
                foreach (var file in command.Files)
                {
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".webp":

                            if (!string.IsNullOrEmpty(oldImage))
                            {
                                _fileManager.DeleteFile(
                                    oldImage,
                                    Directories.PostImage);
                            }

                            break;

                        case ".pdf":

                            if (!string.IsNullOrEmpty(oldPdf))
                            {
                                _fileManager.DeleteFile(
                                    oldPdf,
                                    Directories.PostPdf);
                            }

                            break;

                        case ".txt":

                            if (!string.IsNullOrEmpty(oldTxt))
                            {
                                _fileManager.DeleteFile(
                                    oldTxt,
                                    Directories.PostTxt);
                            }

                            break;

                        case ".doc":
                        case ".docx":

                            if (!string.IsNullOrEmpty(oldWord))
                            {
                                _fileManager.DeleteFile(
                                    oldWord,
                                    Directories.PostWord);
                            }

                            break;

                        case ".zip":

                            if (!string.IsNullOrEmpty(oldZip))
                            {
                                _fileManager.DeleteFile(
                                    oldZip,
                                    Directories.PostZip);
                            }

                            break;
                    }
                }
            }

            return OperationResult.Success();
        }
        public PostFilterDto GetPostByFilter(PostFilterParams filterParams)
        {
            //این خط خیلی  مهم هست 
            //تمامی اطلاعات posts:بر اساس  تاریخ ایجاد از جدید  ترین به قدیمی  ترین  مرتب می کند
            //AsQueryable():
            var result =_context.posts.Include(d => d.Category)
                .Include(d => d.SubCategory).Include(d=>d.User)
              
                .OrderByDescending(d=>d.CreationDate).AsQueryable();
            //اگر  Slug  پر بود
            if (!String.IsNullOrWhiteSpace(filterParams.CategorySlug))
                //نتیجه ی این میشه  یک دونه entity

                result = result.Where(r=>r.Category.Slug == filterParams.CategorySlug 
                || r.SubCategory.Slug==filterParams.CategorySlug);
            if (!String.IsNullOrWhiteSpace(filterParams.Title))

                result = result.Where(r => r.Title.Contains(filterParams.Title));

            var skip = (filterParams.PageID - 1) * filterParams.Take;
            //var PageCount = result.Count() / filterParams.Take;
            //انجام صفحه بندی
            var model = new PostFilterDto()
            {
                //Select(Post=>PostMapper.MapToDto(Post)) این قسمت رو بپرس
                Posts = result.Skip(skip).Take(filterParams.Take).Select(Post => PostMapper.MapToDto(Post)).ToList(),
                FilterParams = filterParams,
              
            };
            model.GeneratePaging(result,filterParams.Take,filterParams.PageID);
            return model;
        }
        public PostDto GetPostById(int postId)
        {
            var Post = _context.posts.Include(c=>c.SubCategory).Include(c=>c.Category).FirstOrDefault(p => p.ID == postId);

            return PostMapper.MapToDto(Post);
        }

        public PostDto GetPostBySlug(string slug)
        {
            var Post = _context.posts.Include(c => c.SubCategory).Include(c => c.Category).Include(c=>c.User).FirstOrDefault(p => p.Slug == slug);

            return PostMapper.MapToDto(Post);
        }
        //آیا این اسلاگ در دیتابیس وجوددارد
        public bool IsSlugExist(string slug, int postId)
        {
            return _context.posts.Any(p =>
                p.Slug == slug.ToSlug()
                &&
                p.ID != postId
            );
        }
        List<PostDto> IPostService.GetRelatedPost(int categoryId )
        {
            return _context.posts.Where(p => p.CategoryID == categoryId || p.SubCategoryID == categoryId).OrderByDescending(c => c.CreationDate).Take(6)
                .Select(Post => PostMapper.MapToDto(Post)).ToList();
        }


        public List<PostDto> GetPopularPost()
        {
            //OrderByDescending:صعودی به نزولی هست
            return _context.posts.Include(c=>c.User).OrderByDescending(c => c.Visit).Take(6)
                .Select(Post => PostMapper.MapToDto(Post)).ToList();
        }

        public void IncreaseVisit(int PostId)
        {
            //اینجا باید  خود پست را دریافت کنیم
            var post = _context.posts.First(p => p.ID == PostId);
            post.Visit += 1;
            _context.SaveChanges();
        }
    }
}


