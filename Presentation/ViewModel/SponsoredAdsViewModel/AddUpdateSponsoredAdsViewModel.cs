using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel.SponsoredAdsViewModel
{
    public class AddUpdateSponsoredAdsViewModel
    {
        public string? SponsoredByName { get; set; }
        public string? AdUrlDestination { get; set; }
        public int AdPrice { get; set; }
        public string? AdStatus { get; set; }
        public string? ShowAdOn { get; set; }
        public string? PublicId { get; set; }
        public IFormFile? File { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Update_At { get; set; }
        public DateTime? Expire_At { get; set; }
    }
}
