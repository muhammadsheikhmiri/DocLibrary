using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Users
{
    public class UserDto
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pasword { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
