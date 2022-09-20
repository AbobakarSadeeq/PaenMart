using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductsInDiscountDealsViewModel
{
    public class ProductsInDiscountViewModel
    {
        public int ProductId { get; set; }
        public int ProductBeforePrice { get; set; }
        public int ProductAfterDiscountPrice { get; set; }
        public int ProductPercentage { get; set; }
    }
}
