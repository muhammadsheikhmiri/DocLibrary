using ProjectChaptereight.BlogWeb.CoreLayer.DTOs.Categories;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Mappers
{
    public class CategoryMapper
    {

        public static CategoryDto Map(Category category)
        {
            return new CategoryDto()
            {
                MetaDescription = category.MetaDescription,
                Title = category.Title,
                Slug = category.Slug,
                MetaTag = category.MetaTag,
                ParentID = category.ParentID,
                Id = category.ID,
            };
        }
    }
}
