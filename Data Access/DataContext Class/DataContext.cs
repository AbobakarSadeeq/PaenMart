using Business_Core.Entities;
using Business_Core.Entities.Carousel;
using Business_Core.Entities.Identity;
using Business_Core.Entities.Identity.AdminAccount;
using Business_Core.Entities.Identity.Email;
using Business_Core.Entities.Identity.user.Employee;
using Business_Core.Entities.Identity.user.Shipper;
using Business_Core.Entities.Identity.UserAddress;
using Business_Core.Entities.Order;
using Business_Core.Entities.Order.OrderDetail;
using Business_Core.Entities.OrderProductReviews;
using Business_Core.Entities.OrderProductReviews.OrderProductReviewsPhoto;
using Business_Core.Entities.Product;
using Business_Core.Entities.Product.Product_Images;
using Business_Core.Entities.ProductWishlist;
using Business_Core.Entities.SponsoredAd;
using Bussiness_Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.DataContext_Class
{
    public class DataContext : IdentityDbContext<CustomIdentity>
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
        public DbSet<Product>  Products { get; set; }
        public DbSet<ProductImages>  ProductImages { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserAddress>  UserAddresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePayment>  EmployeePayments { get; set; }
        public DbSet<Shipper>  Shippers{ get; set; }
        public DbSet<ShipperPayment> ShipperPayments { get; set; }
        public DbSet<AdminAccount>  AdminAccounts { get; set; }
        public DbSet<SendingEmail>  SendingEmails { get; set; }
        public DbSet<Order>  Orders { get; set; }
        public DbSet<OrderDetail>  OrderDetails { get; set; }

        public DbSet<Business_Core.Entities.OrderProductReviews.OrderProductReview>  OrderProductReviews { get; set; }
        public DbSet<Business_Core.Entities.OrderProductReviews.OrderProductReviewsPhoto.OrderProductReviewsPhoto> OrderProductReviewsPhotos { get; set; }

        public DbSet<SponsorsAds>  SponsorsAds { get; set; }

        public DbSet<ProductWishlist>  ProductWishlists { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicFormStructureConfiguration());
            modelBuilder.ApplyConfiguration(new NestSubCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new NestSubCategoryProductBrandConfiguration());
            modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImagesConfiguration());
            modelBuilder.ApplyConfiguration(new CarouselConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeePaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ShipperConfiguration());
            modelBuilder.ApplyConfiguration(new ShipperPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new AdminAccountConfiguration());
            modelBuilder.ApplyConfiguration(new SendingEmailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductReviewConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductReviewsPhotoConfiguration());
            modelBuilder.ApplyConfiguration(new SponsorsAdsConfiguration());
            modelBuilder.ApplyConfiguration(new ProductWishlistConfiguration());




        }


    }
}
