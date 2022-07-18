using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.IdentityViewModel.AdminAccountBalance
{
    public class AddAdminAccountBalanceViewModel
    {
        public int AdminAccountID { get; set; }
        public int CurrentBalance { get; set; }
        public int BeforeBalance { get; set; }
        public string? TransactionPurpose { get; set; } // Client buy product, Add the salary to employee, shipper, Adding new changes to account by admin
        public string? BalanceSituation { get; set; } // add or subtract or before
        public string UserId { get; set; }
        public DateTime? TransactionDateTime { get; set; }
    }
}
