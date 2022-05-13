using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Product.Product_Images
{
    public class ProductImages
    {
        public int ProductImageID { get; set; }
        public string? URL { get; set; }
        public string? PublicId { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }

    public class ProductImagesConfiguration : IEntityTypeConfiguration<ProductImages>
    {
        public void Configure(EntityTypeBuilder<ProductImages> builder)
        {
            builder.HasKey(a => a.ProductImageID);

            // Relationship with brand
            builder.HasOne<Product>(s => s.Product)
                .WithMany(g => g.ProductImages)
                .HasForeignKey(a => a.ProductId)
                .IsRequired(true);


        }
    }
}
