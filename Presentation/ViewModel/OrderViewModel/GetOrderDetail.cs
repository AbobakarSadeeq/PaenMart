using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderViewModel
{
    public class GetOrderDetail
    {
        // Product
        public string ProductName { get; set; }
        public int ProductId { get; set; }

        // Order Detail
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string ProductImageUrl { get; set; }
        public bool QuantityAvailability { get; set; }
    }
}
