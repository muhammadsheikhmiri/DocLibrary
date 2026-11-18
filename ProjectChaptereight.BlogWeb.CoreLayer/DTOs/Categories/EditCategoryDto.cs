using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Categories
{
    public class EditCategoryDto: CreateCategoryDto
    {
        public int Id { get; set; }
    }
}
