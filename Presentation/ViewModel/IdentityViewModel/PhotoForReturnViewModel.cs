using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels.Identity
{
   public class PhotoForReturnViewModel
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string DescriptionHeader { get; set; }
        public DateTime? DateAdded { get; set; }
        public bool IsMainPhoto { get; set; }
        // Get the data from Cloudinary
        public string PublicId { get; set; }
    }
}
