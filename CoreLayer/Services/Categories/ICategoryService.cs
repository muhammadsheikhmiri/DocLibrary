using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Categories;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Services.Categories
{
    public interface ICategoryService
    {
        //در برنامه نویسی  به متد هایی  که قرار است  چیزی را تغییر  دهند  command گفته  می شود 
        //به همین  دلیل  اینجا از  اسم command استفاده کردیم


        //نکته :
        //توی این پروژه  چون  می خواهیم وضعیت ها را چک کنیم  دستورات عملیاتی را ازنوع OperationResult می گذاریم
        OperationResult CreateCategory(CreateCategoryDto command);
        OperationResult EditCategory(EditCategoryDto command);
        List<CategoryDto> GetAllCategory();
        List<CategoryDto> GetChildCategories(int parentId);
        CategoryDto GetCategoryBy(string slug);
        CategoryDto GetCategoryBy(int Id);


        //جلوگیری  از ثبت اسلاگ های  تکراری 
        bool IsSlugExist(string slug);


    }
}
