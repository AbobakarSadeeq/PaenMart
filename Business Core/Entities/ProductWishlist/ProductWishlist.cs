using Business_Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.ProductWishlist
{
    public class ProductWishlist
    {
        public int ProductWishlistID { get; set; }
        public int ProductId { get; set; }
        public virtual Business_Core.Entities.Product.Product Product { get; set; }
        public string UserId { get; set; }
        public virtual CustomIdentity CustomIdentity { get; set; }
        public DateTime? Created_At { get; set; }


    }

    public class ProductWishlistConfiguration : IEntityTypeConfiguration<ProductWishlist>
    {
        public void Configure(EntityTypeBuilder<ProductWishlist> builder)
        {
            builder.HasKey(a => a.ProductWishlistID);


            // Relationship
            builder.HasOne<CustomIdentity>(s => s.CustomIdentity)
                .WithMany(g => g.ProductWishlists)
                .HasForeignKey(a => a.UserId)
                .IsRequired(true);

            // Relationship
            builder.HasOne<Business_Core.Entities.Product.Product>(s => s.Product)
                .WithMany(g => g.ProductWishlists)
                .HasForeignKey(a => a.ProductId)
                .IsRequired(true);





        }
    }
}
