using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderProductReview
{
    public class GetFiveProductsReviewViewModel
    {
        public string ProductName { get; set; } 
        public double RaitingStars { get; set; } 
        public string Comment { get; set; } 
        public int ProductId { get; set; } 
        public string ProductImageUrl { get; set; } 
        public DateTime? ReviewDate { get; set; } 
    }
}
