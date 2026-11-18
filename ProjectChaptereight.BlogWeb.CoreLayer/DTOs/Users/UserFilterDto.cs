using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectChaptereight.BlogWeb.CoreLayer.Utilities;

namespace ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Users
{
    public class UserFilterDto : BasePagination
    {
        public List<UserDto> Users { get; set; }
    }

}