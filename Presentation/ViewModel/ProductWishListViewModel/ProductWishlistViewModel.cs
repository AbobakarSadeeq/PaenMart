using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductWishListViewModel
{
    public class ProductWishlistViewModel
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }

        public DateTime? Created_At { get; set; } = null;

    }
}
