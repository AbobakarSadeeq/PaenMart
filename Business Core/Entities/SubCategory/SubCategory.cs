using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string? SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public DateTime? Created_At { get; set; }
        public virtual ICollection<NestSubCategory> NestSubCategories { get; set; }

        public SubCategory()
        {
            NestSubCategories = new HashSet<NestSubCategory>();
        }
    }

    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            // Primary Key
            builder.HasKey(a => a.SubCategoryID);

            // Relationship
            builder.HasOne<Category>(s => s.Category)
                .WithMany(g => g.SubCategories)
                .HasForeignKey(a => a.CategoryId)
                .IsRequired(true);

            // Columns that required value to be inserted
            builder.Property(p => p.SubCategoryName).IsRequired(true);

        }
    }
}
