using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HBRTEST.Domain;

namespace HBRTEST.Frontend.Models
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