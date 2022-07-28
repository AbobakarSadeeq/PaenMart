using Business_Core.Entities.Identity;
using Business_Core.Entities.Identity.user.Shipper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Order
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? PaymentMethod { get; set; }
        public int? ShipperId { get; set; }
        public virtual Shipper Shipper { get; set; }

        public string CustomIdentityId { get; set; }
        public virtual CustomIdentity CustomIdentity { get; set; }
        public virtual ICollection<Business_Core.Entities.Order.OrderDetail.OrderDetail> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new List<Business_Core.Entities.Order.OrderDetail.OrderDetail>();
        }


    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(a => a.OrderID);



            // Relationship
            builder.HasOne<Shipper>(s => s.Shipper)
                .WithMany(g => g.Orders)
                .HasForeignKey(a => a.ShipperId);

            builder.Property(a => a.ShipperId).IsRequired(false);
            ;

            // Relationship
            builder.HasOne<CustomIdentity>(s => s.CustomIdentity)
                .WithMany(g => g.Orders)
                .HasForeignKey(a => a.CustomIdentityId)
                .IsRequired(true);
        }
    }


}
