using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Options;
using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Users;
using ProjectChaptereight.BlogWeb.CoreLayer.Services;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Categories;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Comments;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.FileManager;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Posts;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Users;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Context;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.IIS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//جهت استفاده از RazorPages
builder.Services.AddRazorPages();
//جهت استفاده از Views
builder.Services.AddControllersWithViews();
//تعریف سرویس add
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMainPageService, MainPageService>();
builder.Services.AddTransient<IPostService,PostService>();
builder.Services.AddTransient<IFileManager,FileManager>();
builder.Services.AddTransient<ICommentService,CommentService>();

//void Authentication(AuthenticationOptions options)
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}
//void ConfigureCookie(CookieAuthenticationOptions options)
//{
//    options.LoginPath = "/Auth/Login";
//    options.LogoutPath = "/Auth/Logout";
//    //بعد از  30 روز منقضی می شود 
//    options.ExpireTimeSpan = TimeSpan.FromDays(30);
//}

//جهت استفاده از تکنولوژی  identity
//برای  لاگین کوکی  تعریف کنم
//startidentity
builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;


})



.AddCookie(options => {
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    //بعد از  30 روز منقضی می شود 
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    //اگر رول مورد نظر را نداشت به این مسیر منتقل می شود 
    options.AccessDeniedPath = "/";
});
//endIdentiy

//جهت اتصال Context به دیتابیس
builder.Services.AddDbContext<BolgContext>(Options=> {

    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddAuthorization(option =>
{
    //این ادمین  پالاسی چک می کند یوزری که می  خوارد  وارد  کنترولر  های ادمین شود حتما دارای ROLE ادمین باشد 
    //انواع و اقسام  پالسی  ها رو داریم 
    option.AddPolicy("AdminPolicy", builder =>
    {
        builder.RequireRole("Admin");
        //اگر اینو بزاری  چک می کنه کاربر  لاگین هست یا خیر 
        //builder.RequireAuthenticatedUser();
}); 
}
);
// محدودیت حجم درخواست در Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 300L * 1024 * 1024;
});

// محدودیت حجم درخواست در IIS
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 300L * 1024 * 1024;
});

// محدودیت حجم فرم‌های چندبخشی و آپلود فایل
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 300L * 1024 * 1024;
});
//جهت استفاده از  مدل mvc این سرویس باید اضافه شود 
//builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

else
{
    //جهت ریدایرکت به صفحات 500
    app.UseExceptionHandler("/ErrorHandler/500");
    app.UseHsts();
}

//جهت ریدایرکت به 404و405
app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();

//app.UseEndpoints(endpoints => {

//    endpoints.MapControllerRoute(name: "default",
//pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

//    //این حط جهت تست  استفاده از  روت پیش فرض بوده است
//    //endpoints.MapAreaControllerRoute(name: "AdminPanel",areaName:"Admin",pattern: "Project/{controller=Home}/{action=Index}/{id?}");
//});
//{ area: exists}:اگر url یک area  داشت آنرا به عنوان یک AREA در  نظربگیر

//نکته:پروژه ما هم Area دارد و هم Controller معمولی.
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}"
//);
//روتینگ ریزورپیج ها
//Razor Pages Routing
app.MapRazorPages();

app.Run();
