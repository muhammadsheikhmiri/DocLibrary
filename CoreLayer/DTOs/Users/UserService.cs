using Microsoft.EntityFrameworkCore;
using ProjectChaptereight.BlogWeb.CoreLayer.Services.Users;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Context;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Users
{
    public class UserService:IUserService
    {
       
        //دراین کلاس جهت ارتباط با دیتابیس  نیاز به context

        private readonly BolgContext _context;



        public UserService(BolgContext context)
        {
            _context = context;
            
        }


        //ورودیش از  جنس UserRegisterDto 
        public OperationResult RegisterUser(UserRegisterDto userRegisterDto)
        {

            //تست متد های entityframework
            //first
            //var first = _context.users.First(u => u.UserName=="ss");



            //firstorDefault
            //var firstorDefault = _context.users.First(u => u.UserName == "ss");
            //Single
            //var Single = _context.users.Single(u => u.UserName == "ss");
            //SingleOrDefault
            //var SingleOrDefault = _context.users.SingleOrDefault(u => u.UserName == "ss");









            /////////////////////////////////

            //جلوگیری از ثبت usernameتکراری
            var isUserNameExist = _context.users.Any(u=>u.UserName==userRegisterDto.UserName);
            if (isUserNameExist)
            {
              return  OperationResult.Error("نام کاربری  تکراری  است");

            }
            //ثبت یوزر 
            //User user = new User();
            //user.UserName=userRegisterDto.UserName;
            //user.FullName=userRegisterDto.Fullname;
            //user.Email=userRegisterDto.Email;   
            //user.Pasword=userRegisterDto.Password;

            //_context.users.Add(user);
            var passwordHash = userRegisterDto.Password.EncodeToMd5();
            //ثبت کاربر
            _context.users.Add(new User()
            {
                FullName = userRegisterDto.Fullname,
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email,
                CreationDate=DateTime.Now,
                IsDeleted = false,
                Role=UserRole.User,
                Pasword = passwordHash,
            });
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditUser(EditUserDto command)
        {
            var user = _context.users.FirstOrDefault(c => c.ID == command.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.FullName = command.FullName;
            user.Role = command.Role;
            _context.SaveChanges();
            return OperationResult.Success();
        }
        public UserDto LoginUser(LoginUserDto loginUserDto)
        {

            //پسوردی  که کاربر وارد کرده است  را هم هش می کند
            var passwordHashed = loginUserDto.Password.EncodeToMd5();
            var user = _context.users.FirstOrDefault(u => u.UserName == loginUserDto.UserName && u.Pasword == passwordHashed);
            if (user == null)
                return null;

            var userDto = new UserDto()
            {

                FullName = user.FullName,
                UserName = loginUserDto.UserName,
                Pasword = user.Pasword,
                Role = user.Role,
                Email = user.Email,
                RegisterDate= user.CreationDate,
                ID = user.ID,


            };
            return userDto;

          


        }
        public UserDto GetUserById(int userId)
        {
            var user = _context.users
                .FirstOrDefault(u => u.ID == userId);
            if (user == null)
                return null;
            return new UserDto()
            {
                FullName = user.FullName,
                Pasword = user.Pasword,
                Role = user.Role,
                UserName = user.UserName,
                RegisterDate = user.CreationDate,
               ID = user.ID
            };
        }
        public UserFilterDto GetUsersByFilter(int pageId, int take)
        {
            var users = _context.users.OrderByDescending(d => d.ID)
                .Where(c => !c.IsDeleted);

            var skip = (pageId - 1) * take;
            var model = new UserFilterDto()
            {
                Users = users.Skip(skip).Take(take).Select(user => new UserDto()
                {
                    FullName = user.FullName,
                    Pasword = user.Pasword,
                    Role = user.Role,
                    UserName = user.UserName,
                    RegisterDate = user.CreationDate,
                    ID = user.ID
                }).ToList()
            };
            model.GeneratePaging(users, take, pageId);
            return model;
        }

    }
}
