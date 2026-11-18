using Microsoft.AspNetCore.Mvc;
using ProjectChaptereight.BlogWeb.Areas.Admin.Models.Categories;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Categories;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Categories;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;

namespace ProjectChaptereight.BlogWeb.Areas.Admin.Controllers
{

    public class CategoryController : AdminControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            //من می خوام لیست تمامی  category  ها رو می خوام نشون بدم
            return View(_categoryService.GetAllCategory());
        }
        //یک روتینگ جدید  برا ی گررفتن ایدی  استفاده می کنیم
        [Route("/admin/category/add/{parentId?}")]
        public IActionResult Add( int? parentId)
        {
            //var model = new CreateCategoryViewModel()
            //{
            //    ParentID = parentId
            //};
            return View();
        }
        [HttpPost("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId, CreateCategoryViewModel createVieModel) {
            //parentId=این parentId  هست که از  توی  روت دریافت کردی
            createVieModel.ParentID=parentId;
            var result = _categoryService.CreateCategory(createVieModel.MapToDto());

            return RedirectAndShowAlert(result, RedirectToAction("Index"));                
                
 
        }
        public IActionResult Edit(int id) {
            var category = _categoryService.GetCategoryBy(id);
            //اگر خالی  بود
            if(category==null)
                return RedirectToAction("Index");

            // تبدیل DTO به ViewModel
            var viewModel = new EditCategoryViewModel()
            {
                //ParentID = category.Id, // حتماً Id را هم ست کنید
                Title = category.Title,
                Slug = category.Slug,
                MetaTag = category.MetaTag,
                MetaDescription = category.MetaDescription
            };
            // حالا مدل درست را به ویو بفرستید
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditCategoryViewModel editModel) {
            var result = _categoryService.EditCategory(new CoreLayer.DTOs.Categories.EditCategoryDto()
            {
               Id= id,
                Title = editModel.Title,
                Slug = editModel.Slug,
                MetaTag = editModel.MetaTag,
                MetaDescription = editModel.MetaDescription
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editModel.Slug),result.Message);
                return View(editModel);
            }
            //اگر  همه چیز با موفقیت انجام شد
            return RedirectToAction("Index");
        }
        public IActionResult GetChildCategories(int ParentId) { 
        var subCategory=_categoryService.GetChildCategories(ParentId);

            return new JsonResult(subCategory);
        
        }
    }
}
