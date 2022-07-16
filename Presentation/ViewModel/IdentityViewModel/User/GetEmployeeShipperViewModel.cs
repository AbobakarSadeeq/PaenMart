using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.IdentityViewModel.User
{
    public class GetEmployeeShipperViewModel
    {
        public int ID { get; set; }
        public string userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DathOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeAddress { get; set; }
        public int Salary { get; set; }
        public string Gender { get; set; }
        public string HireDate { get; set; }
        public string? ShipmentVehicleType { get; set; }  // what type of vehicle using ?
        public string? VehiclePlatNo { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
