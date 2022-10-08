using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderViewModel
{
    public class UpdateOrderDetailsDataViewModel
    {
        // Product
        public string ProductName { get; set; }
        public string ProductSize { get; set; }
        public int ProductId { get; set; }
        public bool? QuantityAvailability { get; set; }


        // Order Detail
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
