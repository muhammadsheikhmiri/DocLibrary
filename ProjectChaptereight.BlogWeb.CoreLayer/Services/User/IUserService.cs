using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Users;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Services.Users
{
    public interface IUserService
    {
        //متد ثبت  نام یوز
        OperationResult RegisterUser(UserRegisterDto registerDto);
        OperationResult EditUser(EditUserDto command);

        //پیدا کردن یوز با مشخصات وارد شده جهت لاگین
        UserDto  LoginUser(LoginUserDto loginUserDto);

        UserDto GetUserById(int userId);
        UserFilterDto GetUsersByFilter(int pageId, int take);
    }
}
