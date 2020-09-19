using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HBRTEST.Entities;

namespace HBRTEST.Models
{
    public class ProductsModel: ProductEntity
    {
        public List<ProductEntity> LstProducts { get; set; }

        public ProductsModel()
        {
            LstProducts = new List<ProductEntity>();
        }
    }
}