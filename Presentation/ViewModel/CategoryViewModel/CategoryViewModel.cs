using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? Created_At { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }

        public CategoryViewModel()
        {
            SubCategories = new HashSet<SubCategory>();
        }
    }
}
