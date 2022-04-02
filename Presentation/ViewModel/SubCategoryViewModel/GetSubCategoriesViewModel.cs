using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class GetSubCategoriesViewModel
    {
        public int SubCategoryID { get; set; }
        public string? SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? Created_At { get; set; }

    }
}
