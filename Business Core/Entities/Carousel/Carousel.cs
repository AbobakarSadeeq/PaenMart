using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Carousel
{
    public class Carousel
    {
        public int CarouselID { get; set; }
        public string? URL { get; set; }
        public string? PublicId { get; set; }
        public int ImagePriority { get; set; }
        public string? ImageTitle { get; set; }
        public string? ImageDescription { get; set; }
        public string? NavigationUrl { get; set; }
    }

    public class CarouselConfiguration : IEntityTypeConfiguration<Carousel>
    {
        public void Configure(EntityTypeBuilder<Carousel> builder)
        {
            builder.HasKey(a => a.CarouselID);

        }
    }
}
