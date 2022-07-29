using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderViewModel
{
    public class GetUserOrderViewModel
    {
        public string CountryName { get; set; }
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDate { get; set; }
        public int OrderItemsCount { get; set; }
    }
}
