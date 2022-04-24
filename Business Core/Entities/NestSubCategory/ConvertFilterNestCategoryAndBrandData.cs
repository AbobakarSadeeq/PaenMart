using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class ConvertFilterNestCategoryAndBrandData
    {
        public ConvertFilterNestCategoryAndBrandData()
        {
            NestSubCategoryWithBrands = new List<FilterNestSubCategoryWithBrand>();
        }
        public string BrandName { get; set; }
        public List<FilterNestSubCategoryWithBrand> NestSubCategoryWithBrands { get; set; }


    }
}
