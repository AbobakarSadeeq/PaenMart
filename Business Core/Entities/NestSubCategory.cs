using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class NestSubCategory
    {
        public int NestSubCategoryID { get; set; }
        public string? NestSubCategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public virtual SubCategory?  SubCategory { get; set; }
        public int DynamicFormStructureId { get; set; }

        public virtual DynamicFormStructure DynamicFormStructure { get; set; }
        public DateTime? Created_At { get; set; }

    

        public virtual ICollection<ProductBrand>  ProductBrands { get; set; }


        public NestSubCategory()
        {
            DynamicFormStructure = new DynamicFormStructure();
            ProductBrands = new HashSet<ProductBrand>();

        }

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

            // Relationship
            builder.HasOne<DynamicFormStructure>(s => s.DynamicFormStructure)
                .WithOne(g => g.NestSubCategory)
                .HasForeignKey<NestSubCategory>(a=>a.DynamicFormStructureId)
                .IsRequired(true);

            // Columns that required value to be inserted
            builder.Property(p => p.NestSubCategoryName).IsRequired(true);

        }
    }
}
