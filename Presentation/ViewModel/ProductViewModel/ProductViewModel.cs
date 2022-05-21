using Business_Core.Entities.Product.Product_Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductViewModel
{
    public class ProductViewModel
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
        public virtual ProductBrandViewModel? ProductBrand { get; set; }
        public int NestSubCategoryId { get; set; }
        public virtual NestSubCategoryViewModel? NestSubCategory { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Modified_at { get; set; }

        // Navigation Properties
        public virtual List<ProductImages>? ProductImages { get; set; }

        public ProductViewModel()
        {
            ProductImages = new List<ProductImages>();
        }

    }
}
