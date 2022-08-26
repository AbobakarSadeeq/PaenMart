using Business_Core.Entities.OrderProductReviews.OrderProductReviewsPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class OrderProductReviewViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public string ProductSingleImageUrl { get; set; }
        public int OrderProductReviewId { get; set; }
        public string ProductComment { get; set; } = "";
        public double RatingStars { get; set; } = 0;

        public ICollection<OrderProductReviewsPhoto> ProductReviewedPhoto { get; set; }
        public OrderProductReviewViewModel()
        {
            ProductReviewedPhoto = new List<OrderProductReviewsPhoto>();
        }

    }
}
