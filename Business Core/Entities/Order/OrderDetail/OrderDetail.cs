using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Order.OrderDetail
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int ProductId { get; set; }
        public Business_Core.Entities.Product.Product? Product { get; set; }
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(a => a.OrderDetailID);


            // Relationship one order have many order-details
            builder.HasOne<Order>(s => s.Order)
                .WithMany(g => g.OrderDetails)
                .HasForeignKey(a => a.OrderId)
                .IsRequired(true);


            // Relationship one product have many order-details
            builder.HasOne<Business_Core.Entities.Product.Product>(s => s.Product)
                .WithMany(g => g.OrderDetails)
                .HasForeignKey(a => a.ProductId)
                .IsRequired(true);
        }
    }
}
