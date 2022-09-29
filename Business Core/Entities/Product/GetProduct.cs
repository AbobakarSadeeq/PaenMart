using Business_Core.Entities.Product.Product_Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Product
{
    public class GetProduct
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDetails { get; set; }
        public int Price { get; set; }
        public string? Color { get; set; }
        public bool StockAvailiability { get; set; }
        public int Quantity { get; set; }
        public int SellUnits { get; set; }

        public int ProductBrandId { get; set; }
        public string? ProductBrandName { get; set; }
        public int NestSubCategoryId { get; set; }
        public string? NestSubCategoryName { get; set; }
        public int Raiting { get; set; }
        public int TotalProductStars { get; set; }
        public bool OnDiscount { get; set; }
        public int AfterDiscountPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime? DiscountExpireAt { get; set; }
        public double ShowStarsByRatings { get; set; } = 0;
        public DateTime? Created_At { get; set; }
        public DateTime? Modified_at { get; set; }
        public virtual IList<GetProductImages> GetProductImagess { get; set; }
        public GetProduct()
        {
            GetProductImagess = new List<GetProductImages>();
        }
    }
}
