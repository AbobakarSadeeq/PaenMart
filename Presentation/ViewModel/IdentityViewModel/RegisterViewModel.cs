using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.IdentityViewModel
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; } // => when fullName is getting from client then remove the space from it to store the name in userName property of identityClass
        [EmailAddress]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation password Do no match.")]
        public string? ConfirmPassword { get; set; }
    }
}
