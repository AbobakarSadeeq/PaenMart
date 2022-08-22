using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderViewModel
{
    public class ShippmentOrderDoneViewModel
    {
        public ShippmentOrderDoneViewModel()
        {
            OrderDetails = new List<GetOrderDetail>();
        }

        public int OrderId { get; set; }
        public string ShipperUserId { get; set; }
        public int OrderTotalPrice { get; set; }

        // customize data
        public string Email { get; set; }
        public string CompleteAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public List<GetOrderDetail> OrderDetails { get; set; }

    }
}
