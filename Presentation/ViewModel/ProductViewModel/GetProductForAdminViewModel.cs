using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.ProductViewModel
{
    public class GetProductForAdminViewModel
    {
        public int ProductID { get; set; } // delete and update findout by productId
        public string? ProductName { get; set; }
        public int Price { get; set; }
        public string? Color { get; set; }
        public bool StockAvailiability { get; set; }
        public int Quantity { get; set; }
        public int SellUnits { get; set; }
        public int ProductBrandId { get; set; }
        public string? ProductBrandName { get; set; }
        public int NestSubCategoryId { get; set; }
        public string? NestCategoryName { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
