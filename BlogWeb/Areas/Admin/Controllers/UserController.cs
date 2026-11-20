using Microsoft.AspNetCore.Mvc;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Users;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Users;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;

namespace ProjectChaptereight.BlogWeb.Areas.Admin.Controllers
{

        public class UserController : AdminControllerBase
        {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1)
        {
            return View(_userService.GetUsersByFilter(pageId, 10));
        }
        [HttpPost]

        public IActionResult Edit(EditUserDto editModel)
        {
            var result = _userService.EditUser(editModel);
            if (result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);
                return RedirectToAction("Index");
            }
            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }

        //پارشیال ویو _EditUser بعد از  پرکردن مقادیر  
        //ریترن  می کند 
        public IActionResult ShowEditModal(int userId)
        {
            var user = _userService.GetUserById(userId);
            return PartialView("_EditUser", new EditUserDto()
            {
                FullName = user.FullName,
                Role = user.Role,
                UserId = userId
            });
        }
    }
}