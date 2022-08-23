using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.OrderProductReviews.OrderProductReviewsPhoto
{
    public class OrderProductReviewsPhoto
    {
        public int OrderProductReviewsPhotoID { get; set; }
        public string? URL { get; set; }
        public string? PublicId { get; set; }
        public int OrderProductReviewId { get; set; }
        public virtual OrderProductReview OrderProductReview {get; set;}

    }

    public class OrderProductReviewsPhotoConfiguration : IEntityTypeConfiguration<OrderProductReviewsPhoto>
    {
        public void Configure(EntityTypeBuilder<OrderProductReviewsPhoto> builder)
        {
            builder.HasKey(a => a.OrderProductReviewsPhotoID);


            // Relationship
            builder.HasOne<OrderProductReview>(s => s.OrderProductReview)
                .WithMany(g => g.Photos)
                .HasForeignKey(a => a.OrderProductReviewId)
                .IsRequired(true);



        }
    }
}
