using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.SponsoredAd
{
    public class SponsorsAds
    {
        public int AdID { get; set; }
        public string? SponsoredByName { get; set; }
        public string? AdUrlDestination { get; set; }
        public int AdPrice { get; set; }
        public string? AdStatus { get; set; }
        public string? ShowAdOnPage { get; set; }
        public string? PublicId { get; set; }
        public string? AdPictureUrl { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Update_At { get; set; }
        public DateTime? Expire_At { get; set; }
    }

    public class SponsorsAdsConfiguration : IEntityTypeConfiguration<SponsorsAds>
    {
        public void Configure(EntityTypeBuilder<SponsorsAds> builder)
        {
            builder.HasKey(a => a.AdID);

            builder.Property(x => x.AdUrlDestination)
           .HasMaxLength(2083);
        }
    }
}
