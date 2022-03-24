using Business_Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.DataContext_Class
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // DB tables
        public DbSet<Category>  Categories { get; set; }
        public DbSet<SubCategory>  SubCategories { get; set; }
        public DbSet<NestSubCategory>  NestSubCategories{ get; set; }
        public DbSet<DynamicFormStructure>  DynamicFormStructures { get; set; }
        public DbSet<ProductBrand>  ProductBrands{ get; set; }
        public DbSet<NestSubCategoryProductBrand> NestSubCategoryProductBrands { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicFormStructureConfiguration());
            modelBuilder.ApplyConfiguration(new NestSubCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new NestSubCategoryProductBrandConfiguration());
            modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());

        }


    }
}
