using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderViewModel
{
    public class ConfirmOrderViewModel
    {
        // User Detail
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        // User Address
        public string CompleteAddress { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string PhoneNumber { get; set; }
        public string OrderDate { get; set; }


        public ICollection<UpdateOrderDetailsDataViewModel> OrderDetail { get; set; }

        public ConfirmOrderViewModel()
        {
            OrderDetail = new List<UpdateOrderDetailsDataViewModel>();
        }
    }
}
