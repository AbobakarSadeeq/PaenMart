using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class DynamicFormStructure
    {
        public int DynamicFormStructureID { get; set; }
        public string? FormStructure { get; set; }
        public int NestSubCategoryId { get; set; }
        public virtual NestSubCategory?  NestSubCategory { get; set; }
        public DateTime? Created_At { get; set; }
        public bool ProductSize { get; set; }

    }

    public class DynamicFormStructureConfiguration : IEntityTypeConfiguration<DynamicFormStructure>
    {
        public void Configure(EntityTypeBuilder<DynamicFormStructure> builder)
        {
            // Primary Key
            builder.HasKey(a => a.DynamicFormStructureID);

            // Relationship
            builder.HasOne<NestSubCategory>(s => s.NestSubCategory)
                .WithOne(g => g.DynamicFormStructure)
                .HasForeignKey<DynamicFormStructure>(a=>a.NestSubCategoryId)
                .IsRequired(true);

            // Columns that required value to be inserted
            builder.Property(p => p.FormStructure).IsRequired(true);

        }
    }
}
