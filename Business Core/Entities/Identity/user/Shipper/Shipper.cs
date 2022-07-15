using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Identity.user.Shipper
{
    public class Shipper
    {
        public int ShipperID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DathOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? HomeAddress { get; set; }
        public int Salary { get; set; }
        public string? Gender { get; set; }
        public string? ShipperHireDate { get; set; }
        public string? ShipmentVehicleType { get; set; }  // what type of vehicle using ?
        public string? VehiclePlatNo { get; set; }
        public int NumberOfShipmentsDone { get; set; } // when shiper done shipment 
        public string? UserId { get; set; }
        public virtual CustomIdentity? User { get; set; }
        public virtual ICollection<ShipperPayment>? ShipperPayments { get; set; }
    }
    public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.HasKey(a => a.ShipperID);

            // Relationship
            builder.HasOne<CustomIdentity>(s => s.User)
                .WithOne(g => g.Shipper)
                .HasForeignKey<Shipper>(a => a.UserId)
                .IsRequired(true);

        }
    }
}
