using Business_Core.Entities.Product.Product_Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Product
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDetails { get; set; }
        public int Price { get; set; }
        public string? Color { get; set; }
        public bool StockAvailiability { get; set; }
        public int Quantity { get; set; }
        public int SellUnits { get; set; }

        public int ProductBrandId { get; set; }
        public virtual ProductBrand?  ProductBrand { get; set; }
        public int NestSubCategoryId { get; set; }
        public virtual NestSubCategory?  NestSubCategory { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Modified_at { get; set; }

        // Navigation Properties
        public virtual IList<ProductImages>?  ProductImages { get; set; }

        public Product()
        {
            ProductImages = new List<ProductImages>();
        }


    }


    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(a => a.ProductID);

            // Relationship with brand
            builder.HasOne<ProductBrand>(s => s.ProductBrand)
                .WithMany(g => g.Products)
                .HasForeignKey(a => a.ProductBrandId)
                .IsRequired(true);


            // Relationship with NestSubCategory
            builder.HasOne<NestSubCategory>(s => s.NestSubCategory)
                .WithMany()
                .HasForeignKey(a => a.NestSubCategoryId)
                .IsRequired(true);
        }
    }
}
