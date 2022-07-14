using Business_Core.Entities.Identity;
using Business_Core.Entities.Identity.UserAddress;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Core.Entities
{
   public class UserAddress
    {
        [Key]
        public int UserAddressID { get; set; }
        public string? CompleteAddress { get; set; }
        public string? PhoneNumber { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City? City { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual CustomIdentity? User  { get; set; }


    }


}
