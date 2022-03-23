using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class SubCategoryViewModel
    {
        public int SubCategoryID { get; set; }
        public string? SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime? Created_At { get; set; }
        public ICollection<NestSubCategory> NestSubCategories { get; set; }

        public SubCategoryViewModel()
        {
            NestSubCategories = new HashSet<NestSubCategory>();
        }
    }
}
