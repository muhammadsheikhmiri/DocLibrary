using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Users;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Users;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace ProjectChaptereight.BlogWeb.Pages.Auth
{
    //ساخت یک توکن امنیتی
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class RegisterModel : PageModel
    {

 
          #region Properties

    [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "کلمه عبور")]
        [MinLength(6, ErrorMessage = "{0} باید حداقل 6 کاراکتر باشد")]
        public string Password { get; set; }

        #endregion


        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid) {

                //پیج همین صفحه ی جاری هست
                return Page();
            }
            var result = _userService.RegisterUser(new UserRegisterDto()
            {

                UserName = UserName,
                Password = Password,
                Email = Email,
                Fullname = FullName


            });
            //برگرداندان خطا به کاربر
            if (result.Status == CoreLayer.Utilities.OperationResultStatus.Error)
            {

                ModelState.AddModelError("UserName", result.Message);
                //Pageمنظورش همین صفحه است
                return Page();
            }

            //لاگین موفقیت آمیز
            //اگر ثبت نام با مووفقیت انجام شد
            return RedirectToPage("Login");
        }
    }
}
