using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class GetDynamicFormStructureViewModel
    {
        public int DynamicFormStructureID { get; set; }
        public string? FormStructure { get; set; }
        public int NestSubCategoryId { get; set; }
        public string? NestSubCategoryName { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
