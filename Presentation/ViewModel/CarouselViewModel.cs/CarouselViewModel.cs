using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.CarouselViewModel.cs
{
    public class CarouselViewModel
    {
        public int CarouselID { get; set; }
        public string? URL { get; set; }
        public string? PublicId { get; set; }
        public int ImagePriority { get; set; }
        public string? ImageTitle { get; set; }
        public string? ImageDescription { get; set; }
        public string? NavigationUrl { get; set; }
        public virtual IFormFile? File { get; set; }

    }
}
