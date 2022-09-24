using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class ProductDiscountDeal
    {
        public int ProductDiscountDealID { get; set; }
        public int ProductBeforePrice { get; set; }
        public int ProductAfterDiscountPrice { get; set; }
        public int ProductPercentage { get; set; }
        public int DiscountDealId { get; set; }
        public Business_Core.Entities.DiscountDeal.DiscountDeal? DiscountDeal { get; set; }
        public int ProductId { get; set; }
        public virtual Business_Core.Entities.Product.Product? Product { get; set; }
    }

    public class ProductDiscountDealConfiguration : IEntityTypeConfiguration<ProductDiscountDeal>
    {
        public void Configure(EntityTypeBuilder<ProductDiscountDeal> builder)
        {
            builder.HasKey(a => a.ProductDiscountDealID);

            builder.HasOne<Business_Core.Entities.Product.Product>(s => s.Product)
             .WithMany(g => g.ProductDiscountDeals)
             .HasForeignKey(a => a.ProductId)
             .IsRequired(true);

            builder.HasOne<Business_Core.Entities.DiscountDeal.DiscountDeal>(s => s.DiscountDeal)
            .WithMany(g => g.ProductDiscountDeals)
            .HasForeignKey(a => a.DiscountDealId)
            .IsRequired(true);
        }
    }


}
