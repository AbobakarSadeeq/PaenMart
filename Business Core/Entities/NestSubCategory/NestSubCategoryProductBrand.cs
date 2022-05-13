using Business_Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class NestSubCategoryProductBrand
    {
        public int NestSubCategoryId { get; set; }
        public NestSubCategory? NestSubCategory { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand? ProductBrand { get; set; }
        public DateTime? Created_At { get; set; }
    }

    public class NestSubCategoryProductBrandConfiguration : IEntityTypeConfiguration<NestSubCategoryProductBrand>
    {
        public void Configure(EntityTypeBuilder<NestSubCategoryProductBrand> builder)
        {
            builder.HasKey(a => new {a.NestSubCategoryId, a.ProductBrandId});

            // Relationship
            builder.HasOne(s => s.NestSubCategory)
                .WithMany(s => s.NestSubCategoryProductBrand)
                .HasForeignKey(a => a.NestSubCategoryId)
                .IsRequired(true);

            builder.HasOne(s => s.ProductBrand)
                .WithMany(s => s.NestSubCategoryProductBrand)
                .HasForeignKey(a => a.ProductBrandId)
                .IsRequired(true);


        }

    }

}
