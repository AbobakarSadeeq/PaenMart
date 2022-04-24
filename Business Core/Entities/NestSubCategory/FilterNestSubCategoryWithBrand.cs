using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class FilterNestSubCategoryWithBrand
    {
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public int NestCategoryId { get; set; }
        public string? NestCategoryName { get; set; }
    }
}
