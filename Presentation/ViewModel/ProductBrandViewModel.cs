using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class ProductBrandViewModel
    {
        public int ProductBrandID { get; set; }
        public string? BrandName { get; set; }
        public int NestSubCategoryId { get; set; }
        public NestSubCategory? NestSubCategory { get; set; }
        public DateTime? Created_At { get; set; }

    }
}
