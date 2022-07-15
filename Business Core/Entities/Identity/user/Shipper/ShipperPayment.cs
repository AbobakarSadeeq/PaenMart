using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Identity.user.Shipper
{
    public class ShipperPayment
    {
        public int ShipperMonthlyPaymentID { get; set; }
        public bool Payment { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Payment_At { get; set; }
        public int ShipperId { get; set; }
        public virtual Shipper? Shipper { get; set; }
    }
    public class ShipperPaymentConfiguration : IEntityTypeConfiguration<ShipperPayment>
    {
        public void Configure(EntityTypeBuilder<ShipperPayment> builder)
        {
            builder.HasKey(a => a.ShipperMonthlyPaymentID);

            // Relationship
            builder.HasOne<Shipper>(s => s.Shipper)
                .WithMany(g => g.ShipperPayments)
                .HasForeignKey(a => a.ShipperId)
                .IsRequired(true);

        }
    }
}
