using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Users;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Users;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.Pages.Auth
{

    //ساخت یک توکن امنیتی
    [ValidateAntiForgeryToken]
    //[BindProperties]
    public class LoginModel : PageModel
    {

        //تزیرق سرویس
      private readonly  IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [Required(ErrorMessage ="{0}راواردنمایید")]
        [Display(Name="نام کاربری")]
        [BindProperty]
        public string UsreName { get; set; }

        [Required(ErrorMessage = "{0}راواردنمایید")]
        [Display(Name = "کلمه ی عبور")]
        [BindProperty]
        [MinLength(6,ErrorMessage =("کلمه ی عبور باید بیشتر از  5 کاراکتر باشد"))]
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            //نتیجه ی بررسی  Required یا دیگر ویژگی ها به  داخل ModelStateریخته می شود 
            //اگر مقداری اشتیاه وارد شده باشد مجدد با برگشتن به پیج به کاربر نمایش داده می شود
            if (!ModelState.IsValid)
            {

                //پیج همین صفحه ی جاری هست
                return Page();
            }
            var user = _userService.LoginUser(new LoginUserDto()
            {
                Password = Password,
                UserName = UsreName
            });
            //اگر نتیجه لاگین بگوید کاربر پیدا نشد → خطا نشان بده.
            if (user==null)
            {
                ModelState.AddModelError(key: "UsreName", errorMessage: "کاربری  با مشخصات وارد شده یافت نشد");


                return Page();
            }


            var claims = new List<Claim>()
    {
        new Claim("Test", "Test"),
        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
        new Claim(ClaimTypes.Name, user.FullName ?? user.UserName), // اگر Fullname نال بود، از Username استفاده کن
        //اینجا کوکی  درست شده 
        new Claim(ClaimTypes.Role,user.Role.ToString())
    };
            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            //principal:اطلاعات کاربر را  می گیرد 
            var Claimsprincipal = new ClaimsPrincipal(identity);
            //برای ذخیره  کرد ن پسورد کاربران
            var PeroPerties = new AuthenticationProperties()
            //Object Initializer
            {
                //IsPersistent=true:رمز کاربر  ذخیره  می شود
                IsPersistent = true,
            };
            //SignInAsync() = ایجاد کردن کوکی لاگین برای کاربر
            //احراز هویت عملیات  
            await HttpContext.SignInAsync(Claimsprincipal, PeroPerties);
            TempData["LoginSuccess"] = "ورود با موفقیت انجام شد";
            //براساس نام پیج ریدایرکت کردیم
            //return RedirectToPage("../Index");
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}
