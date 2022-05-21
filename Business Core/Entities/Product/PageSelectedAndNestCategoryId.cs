using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Product
{
    public class PageSelectedAndNestCategoryId
    {
        public int NestCategoryId { get; set; }
        public int PageSelectedNo { get; set; }
        public int singleCategoryTotalProductsCount { get; set; }
    }
}
