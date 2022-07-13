using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.IdentityViewModel.User
{
    public class CityViewModel
    {
        public int CityID { get; set; }
        public int CountryId { get; set; }
        public string? CityName{ get; set; }
        public string? CountryName{ get; set; }
    }
}
