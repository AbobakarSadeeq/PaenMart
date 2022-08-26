using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.OrderProductReview
{
    public class AddUserOrderProductReview
    {

        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int RatingStars { get; set; }
        public string? ProductComment { get; set; }
        public string? ReviewStatus { get; set; }
        public int OrderProductReviewId { get; set; }
        public DateTime? Add_Review_Date { get; set; } // when review is added by uer 
        public IList<IFormFile> File { get; set; }
    }
}
