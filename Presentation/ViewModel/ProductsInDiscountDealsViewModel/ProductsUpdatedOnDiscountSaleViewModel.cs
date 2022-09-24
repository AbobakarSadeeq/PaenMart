using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductsInDiscountDealsViewModel
{
    public class ProductsUpdatedOnDiscountSaleViewModel
    {
        public int ProductBeforePrice { get; set; }
        public int ProductAfterDiscountPrice { get; set; }
        public int ProductPercentage { get; set; }
        public int DiscountDealId { get; set; }
        public int ProductId { get; set; }
    }
}
