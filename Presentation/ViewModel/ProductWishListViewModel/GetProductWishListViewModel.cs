using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductWishListViewModel
{
    public class GetProductWishListViewModel
    {
        public int ProductId { get; set; }
        public int ProductWishlistID { get; set; }
        public DateTime? Created_At { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public int ProductPrice { get; set; }
        public int AfterDiscountPrice { get; set; } = 0;
        public int DiscountPercentage { get; set; } = 0;
    }
}
