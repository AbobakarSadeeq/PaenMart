using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderViewModel
{
    public class ShippmentOrderDoneViewModel
    {
        public int OrderId { get; set; }
        public string ShipperUserId { get; set; }
        public int OrderTotalPrice { get; set; }
    }
}
