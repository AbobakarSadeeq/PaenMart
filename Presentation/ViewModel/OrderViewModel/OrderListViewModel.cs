using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderViewModel
{
    public class OrderListViewModel
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public string CountryName { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public int? ShipperId { get; set; }
        public int OrderItemsCount { get; set; }
    }
}
