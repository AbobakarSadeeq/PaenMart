using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class NestSubCategoryProductBrandJoining
    {
        public int NestSubCategoryId { get; set; }
        public int ProductBrandId { get; set; }
        public string? BrandName { get; set; }
        public string? NestSubCategoryName { get; set; }
        public DateTime? NestSubCategoryProductBrandCreated_At { get; set; }
    }
}
