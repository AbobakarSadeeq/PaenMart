using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class GetNestSubCategoryViewModel
    {
        public int NestSubCategoryID { get; set; }
        public string? NestSubCategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
