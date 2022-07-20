using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.IdentityViewModel.User.Employee
{
    public class GetEmployeesPayment
    {
        public int EmployeeID { get; set; }
        public int EmployeePaymentId { get; set; }

        public DateTime? Payment_At { get; set; }
        public string? PaymentStatus { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int Salary { get; set; }

    }
}
