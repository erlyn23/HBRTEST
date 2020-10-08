using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HBRTEST.Domain;

namespace HBRTEST.Models
{
    public class CategoryModel: CategoryEntity
    {
        public List<CategoryEntity> LstCategories { get; set; }

        public CategoryModel()
        {
            LstCategories = new List<CategoryEntity>();
        }
    }
}