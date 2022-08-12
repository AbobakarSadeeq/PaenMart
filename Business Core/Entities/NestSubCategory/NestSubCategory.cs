using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Business_Core.Entities;

namespace Business_Core.Entities
{
    public class NestSubCategory
    {
        public int NestSubCategoryID { get; set; }
        public string? NestSubCategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public virtual SubCategory? SubCategory { get; set; }
        public virtual DynamicFormStructure? DynamicFormStructure { get; set; }
        public virtual ICollection<NestSubCategoryProductBrand>? NestSubCategoryProductBrand { get; set; }
        public virtual ICollection<Business_Core.Entities.Product.Product>? Products { get; set; }

        public DateTime? Created_At { get; set; }

    }

    public class NestSubCategoryConfiguration : IEntityTypeConfiguration<NestSubCategory>
    {
        public void Configure(EntityTypeBuilder<NestSubCategory> builder)
        {
            // Primary Key
            builder.HasKey(a => a.NestSubCategoryID);

            // Relationship
            builder.HasOne<SubCategory>(s => s.SubCategory)
                .WithMany(g => g.NestSubCategories)
                .HasForeignKey(a => a.SubCategoryId)
                .IsRequired(true);




            // Columns that required value to be inserted
            builder.Property(p => p.NestSubCategoryName).IsRequired(true);

        }
    }
}
