using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels.Identity
{
   public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public bool IsSelected { get; set; }
    }
}
