using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public class DynamicFormStructureViewModel
    {
        public int DynamicFormStructureID { get; set; }
        public string? FormStructure { get; set; }
        public int NestCategoryId { get; set; }
        public NestSubCategory? NestSubCategory { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
