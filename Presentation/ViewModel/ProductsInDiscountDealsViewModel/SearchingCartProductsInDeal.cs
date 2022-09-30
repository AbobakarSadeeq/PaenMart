using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductsInDiscountDealsViewModel
{
    public class SearchingCartProductsInDeal
    {
        public int ProductId { get; set; }
        public bool ProductInDeal { get; set; }
        public int AfterDiscountPrice { get; set; } = 0;
        public int DiscountPercentage { get; set; } = 0;
    }
}
