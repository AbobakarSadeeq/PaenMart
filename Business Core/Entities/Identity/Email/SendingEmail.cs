using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Identity.Email
{
    public class SendingEmail
    {
        public int SendingEmailID { get; set; }
        public string AppPassword { get; set; }
        public string OwnerEmail { get; set; }

    }
    public class SendingEmailConfiguration : IEntityTypeConfiguration<SendingEmail>
    {
        public void Configure(EntityTypeBuilder<SendingEmail> builder)
        {
            builder.HasKey(a => a.SendingEmailID);
        }
    }
}
