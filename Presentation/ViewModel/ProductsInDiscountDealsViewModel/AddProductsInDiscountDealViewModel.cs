using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductsInDiscountDealsViewModel
{
    public class AddProductsInDiscountDealViewModel
    {
        public string? DealName { get; set; }
        public DateTime? DealExpireAt { get; set; }
        public DateTime? DealCreatedAt { get; set; } = DateTime.Now;
        public ICollection<ProductsInDiscountViewModel>? SelectedProductsInDeal { get; set; }
    }
}
