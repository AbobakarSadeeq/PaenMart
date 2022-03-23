using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class NestSubCategoryViewModel
    {
        public int NestSubCategoryID { get; set; }
        public string? NestSubCategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int DynamicFormStructureId { get; set; }

        public DynamicFormStructure DynamicFormStructure { get; set; }
        public DateTime? Created_At { get; set; }



        public ICollection<ProductBrand> ProductBrands { get; set; }


        public NestSubCategoryViewModel()
        {
            DynamicFormStructure = new DynamicFormStructure();
            ProductBrands = new HashSet<ProductBrand>();

        }
    }
}
