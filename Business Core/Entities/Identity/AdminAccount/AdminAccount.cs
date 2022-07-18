using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Identity.AdminAccount
{
    public class AdminAccount
    {
        public int AdminAccountID { get; set; }
        public int CurrentBalance { get; set; }
        public int BeforeBalance { get; set; }
        public string? TransactionPurpose { get; set; } // Client buy product, Add the salary to employee, shipper, Adding new changes to account by admin
        public string? BalanceSituation { get; set; } // add or subtract or before
        public string UserId { get; set; }
        public CustomIdentity CustomIdentity { get; set; }
        public DateTime? TransactionDateTime { get; set; }  

        // admin have to select how many transaction want checkout

    }

    public class AdminAccountConfiguration : IEntityTypeConfiguration<AdminAccount>
    {
        public void Configure(EntityTypeBuilder<AdminAccount> builder)
        {
            builder.HasKey(a => a.AdminAccountID);

            // Relationship
            builder.HasOne<CustomIdentity>(s => s.CustomIdentity)
                .WithMany(g => g.AdminAccounts)
                .HasForeignKey(a => a.UserId)
                .IsRequired(true);

        }
    }
}
