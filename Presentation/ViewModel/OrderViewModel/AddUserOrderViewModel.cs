using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderViewModel
{
    public class AddUserOrderViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int DiscountPercentage { get; set; }
        public int AfterDiscountPrice { get; set; }
        public string PaymentMethod { get; set; }

        // sending this data in email thats why required
        public string UserEmail { get; set; }
        public string FullName { get; set; }
        public string UserAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductSize { get; set; }


    }
}
