using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Categories;
using ProjectChaptereight.BlogWeb.CoreLayer.Mappers;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Context;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ProjectChaptereight.BlogWeb.CoreLayer.Services.Categories
{



    public class CategoryService : ICategoryService
    {
        private readonly BolgContext _context;

        public CategoryService(BolgContext context)
        {
            _context = context;
        }

        public bool IsSlugExist(string slug)
        {
            return _context.categories.Any(c => c.Slug == slug.ToSlug());
        }
    
        OperationResult ICategoryService.CreateCategory(CreateCategoryDto command)
        {
            if (IsSlugExist(command.Slug))
                return OperationResult.Error("این اسلاگ از  قبل وجود دارد");

            _context.categories.Add(new Category()
            {

                Title = command.Title,
                IsDeleted=false,
                ParentID= command.ParentID,
                Slug = command.Slug.ToSlug(),
                MetaTag = command.MetaTag,
                MetaDescription = command.MetaDescription,

            });
            _context.SaveChanges();
           return  OperationResult.Success();

        }
        OperationResult ICategoryService.EditCategory(EditCategoryDto command)
        {
            //برای  پیدا کردن آیدی =SingleOrDefault بهتر است
            var category = _context.categories.FirstOrDefault(c=>c.ID==command.Id);
            if(category==null)
               return OperationResult.NotFound("لطفایک سطر راانتخاب نمایید");

            //category.Slug:این چیزی  هست  که از کاربر  گرفتیم  
            //command.Slug:این چیزی  هست 
            if (command.Slug.ToSlug()!= category.Slug)

                if (IsSlugExist(command.Slug))
                    return OperationResult.Error("این اسلاگ از  قبل وجود دارد");

            //دستورات آپدیت
            category.MetaDescription = command.MetaDescription;
            category.MetaTag = command.MetaTag;
            category.Slug = command.Slug.ToSlug();
            category.Title = command.Title;

            //Parentid=برای  زیرمجموعه ها  می باشد 
            //نیاز یه آپدیت  توسط کاربر  نمی باشد
            //چون cahngetrakerActive هست 
            //_context.Update(category);

            _context.SaveChanges();
            return OperationResult.Success();
        }

        List<CategoryDto> ICategoryService.GetAllCategory()
        {
            //CategoryDto چون خروجی  این بود 
            //اصطلاح میشه map کردن به  CategoryDto
            //اصطلاحا به این کار می گن map کردن

            //روی اول
            //return _context.categories.Where(category=>category.IsDeleted==false).Select(c=> new CategoryDto() {

            //  MetaDescription = c.MetaDescription,
            //  Title = c.Title,
            //  Slug= c.Slug,
            //  MetaTag = c.MetaTag,
            //  ParentID = c.ParentID,
            // Id=c.ID, 
            //}).ToList();


            //روش  mapper
            return _context.categories.Where(category => category.IsDeleted == false).Select(c=>CategoryMapper.Map(c)).ToList();

            }

        CategoryDto ICategoryService.GetCategoryBy(string slug)
        {
            var category = _context.categories.FirstOrDefault(c => c.Slug == slug);
            if (category == null)
                return null;
            return CategoryMapper.Map(category);
        }

        CategoryDto ICategoryService.GetCategoryBy(int Id)
        {
            var category = _context.categories.FirstOrDefault(c => c.ID==Id);
            if (category == null)
                return null;
            return CategoryMapper.Map(category);
        }

        List<CategoryDto> ICategoryService.GetChildCategories(int parentId)
        {
            return _context.categories.Where(category => category.ParentID == parentId)
                .Select(c => CategoryMapper.Map(c)).ToList();

        }


        bool ICategoryService.IsSlugExist(string slug)
        {
            throw new NotImplementedException();
        }
    }
}
