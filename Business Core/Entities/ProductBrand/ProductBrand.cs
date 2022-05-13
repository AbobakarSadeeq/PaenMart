using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Core.Entities;

namespace Business_Core.Entities.Product
{
    public class ProductBrand
    {
        public int ProductBrandID { get; set; }
        public string? BrandName { get; set; }
        public virtual ICollection<NestSubCategoryProductBrand>? NestSubCategoryProductBrand { get; set; }
        public virtual ICollection<Product> Products { get; set; }


        public DateTime? Created_At { get; set; }


    }

    public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.HasKey(a => a.ProductBrandID);

            builder.Property(a => a.BrandName).IsRequired(true);
        }
    }
}
