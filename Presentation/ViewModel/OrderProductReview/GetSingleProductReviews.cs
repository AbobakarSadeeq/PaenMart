 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderProductReview
{
    public class GetSingleProductReviews
    {
        public int ProductId { get; set; }
        public string ProductComment { get; set; } = "";
        public double RatingStars { get; set; } = 0;
        public string UserFullName { get; set; }
        public ICollection<GetSingleProductReviewPhotos> ProductReviewedPhoto { get; set; }
    }
}
