using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.IdentityViewModel.User.Shipper
{
    public class GetShipperPaymentViewModel
    {
        public int ShipperID { get; set; }
        public int ShipperPaymentId { get; set; }

        public DateTime? Payment_At { get; set; }
        public string? PaymentStatus { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int Salary { get; set; }
    }
}
