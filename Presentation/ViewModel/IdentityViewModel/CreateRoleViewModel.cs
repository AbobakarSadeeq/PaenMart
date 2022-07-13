using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels.Identity
{
   public class CreateRoleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
