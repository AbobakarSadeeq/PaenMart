using Business_Core.Entities.Product.Product_Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductViewModel
{
    public class GetProductViewModel
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int Price { get; set; }
        public string? Color { get; set; }
        public int ProductBrandId { get; set; }
        public string? ProductBrandName { get; set; }
        public string? ImageUrl { get; set; }
        public int Raiting { get; set; }
        public int TotalProductStars { get; set; }
        public double ShowStarsByRatings { get; set; }

        public virtual IList<GetProductImages> GetProductImagess { get; set; }
        public GetProductViewModel()
        {
            GetProductImagess = new List<GetProductImages>();
        }

    }
}
