using Business_Core.Entities.Identity.user.Employee;
using Business_Core.Entities.Identity.user.Shipper;
using Business_Core.Entities.OrderProductReviews;
using Bussiness_Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Identity
{
    public class CustomIdentity : IdentityUser
    {
        public string? FullName { get; set; }
        public virtual ICollection<UserImage>? UserImages { get; set; }
        public virtual Bussiness_Core.Entities.UserAddress? Address { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Shipper  Shipper { get; set; }
        public virtual ICollection<Business_Core.Entities.Identity.AdminAccount.AdminAccount>?  AdminAccounts{ get; set; }
        public virtual ICollection<Business_Core.Entities.Order.Order>?  Orders{ get; set; }

        public virtual ICollection<OrderProductReview>? OrderProductReview { get; set; }

        public CustomIdentity()
        {
            UserImages = new HashSet<UserImage>();
        }
    }

}
