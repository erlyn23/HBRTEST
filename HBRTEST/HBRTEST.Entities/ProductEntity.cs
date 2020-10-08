using System;
using System.Collections.Generic;
using System.Text;

namespace HBRTEST.Domain
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public int Existence { get; set; }
        public float Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public string Status { get; set; }
    }
}
