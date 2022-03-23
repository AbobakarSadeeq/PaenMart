using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class ProductBrand
    {
        public int ProductBrandID { get; set; }
        public string? BrandName{ get; set; }
        public int NestSubCategoryId { get; set; }
        public virtual NestSubCategory? NestSubCategory { get; set; }
        public DateTime? Created_At { get; set; }

        
    }

    public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.HasKey(a => a.ProductBrandID);

            // Relationship
            builder.HasOne<NestSubCategory>(s => s.NestSubCategory)
                .WithMany(g => g.ProductBrands)
                .HasForeignKey(a => a.NestSubCategoryId)
                .IsRequired(true);

            builder.Property(a => a.BrandName).IsRequired(true);
        }
    }
}
