using Business_Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.OrderProductReviews
{
    public class OrderProductReview
    {
        public int OrderProductReviewID { get; set; }
        public string UserId { get; set; }
        public virtual CustomIdentity CustomIdentity { get; set; }
        public int ProductId { get; set; }
        public virtual Business_Core.Entities.Product.Product Product { get; set; }
        public double RaitingStars { get; set; }
        public string? ProductComment { get; set; }
        public string? ReviewStatus { get; set; }
        public DateTime? Add_Review_Date { get; set; } // when review is added by uer 
        public virtual ICollection<Business_Core.Entities.OrderProductReviews.OrderProductReviewsPhoto.OrderProductReviewsPhoto> Photos { get; set; }
    }


    public class OrderProductReviewConfiguration : IEntityTypeConfiguration<OrderProductReview>
    {
        public void Configure(EntityTypeBuilder<OrderProductReview> builder)
        {
            builder.HasKey(a => a.OrderProductReviewID);


            // Relationship
            builder.HasOne<Business_Core.Entities.Product.Product>(s => s.Product)
                .WithMany(g => g.OrderProductReview)
                .HasForeignKey(a => a.ProductId)
                .IsRequired(true);

            // Relationship
            builder.HasOne<CustomIdentity>(s => s.CustomIdentity)
                .WithMany(g => g.OrderProductReview)
                .HasForeignKey(a => a.UserId)
                .IsRequired(true);

        }
    }
}
