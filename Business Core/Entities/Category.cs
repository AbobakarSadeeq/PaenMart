using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? Created_At { get; set; }
        public virtual ICollection<SubCategory>  SubCategories { get; set; }

        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(a => a.CategoryID);

            builder.Property(a => a.CategoryName).IsRequired(true);
        }
    }
}
