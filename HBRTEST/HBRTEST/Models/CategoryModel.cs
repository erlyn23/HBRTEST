using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HBRTEST.Entities;

namespace HBRTEST.Models
{
    public class CategoryModel: CategoryEntity
    {
        public List<CategoryEntity> lstCategories { get; set; }

        public CategoryModel()
        {
            lstCategories = new List<CategoryEntity>();
        }
    }
}