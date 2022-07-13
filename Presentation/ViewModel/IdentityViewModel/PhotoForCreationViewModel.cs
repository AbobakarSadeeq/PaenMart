using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels.Identity
{
   public class PhotoForCreationViewModel
    {
        public int Id { get; set; }
        // when we want to Insert photo then we should send some properties to the API not All photo Detail.
        public string? URL { get; set; }
        public IFormFile File { get; set; } // Photo that we want to uploading and it will be send with HTTP request.
        public string? Description { get; set; }
        public string? DescriptionHeader { get; set; }
        public DateTime? DateAdded { get; set; } = DateTime.Now;
        public string? PublicId { get; set; }
        public string? CustomIdentityId { get; set; }
    }
}
