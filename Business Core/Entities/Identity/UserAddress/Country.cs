using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Identity.UserAddress
{
    public class Country
    {
        public int CountryID { get; set; }
        public string? CountryName { get; set; }
        public virtual ICollection<City>? Cities { get; set; }
    }
}
