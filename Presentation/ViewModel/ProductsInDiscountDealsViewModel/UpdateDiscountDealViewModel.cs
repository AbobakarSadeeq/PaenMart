using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductsInDiscountDealsViewModel
{
    public class UpdateDiscountDealViewModel
    {
        public DateTime? ExpireAt { get; set; }
        public string? DealName { get; set; }
        public int DiscountDealId { get; set; }
        public ICollection<ProductsUpdatedOnDiscountSaleViewModel>? UpdateProductDiscountDealList { get; set; }
    }
}
