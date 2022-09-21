using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductsInDiscountDealsViewModel
{
    public class GetDiscountDealDetailViewModel
    {
        public string ProductImageUrl { get; set; }
        public string ProductName { get; set; }
        public int DiscountPercentage { get; set; }
        public int BeforePrice { get; set; }
        public int AfterPrice { get; set; }
        public int ProductId { get; set; }

        public string DealName { get; set; }
        public int ProductsLiveCount { get; set; }
        public DateTime? Expire_At { get; set; }
        public DateTime? Created_At { get; set; }

    }
}
