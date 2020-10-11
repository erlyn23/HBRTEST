using HBRTEST.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBRTEST.Frontend.Models
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