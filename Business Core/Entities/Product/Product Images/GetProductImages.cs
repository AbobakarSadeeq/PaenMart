using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Product.Product_Images
{
    public class GetProductImages
    {
        public int ProductImageID { get; set; }
        public string URL { get; set; }
        public string PublicId { get; set; }
        public int ProductId { get; set; }
    }
}
