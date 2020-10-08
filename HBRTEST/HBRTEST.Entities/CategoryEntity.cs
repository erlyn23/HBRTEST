using System;
using System.Collections.Generic;
using System.Text;

namespace HBRTEST.Domain
{
    public class CategoryEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationeDate { get; set; }
        public string Status { get; set; }
    }
}
