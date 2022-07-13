using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Identity.UserAddress
{
    public class City
    {
        public int CityID { get; set; }
        public string? CityName { get; set; }
        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }
    }

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // Primary Key
            builder.HasKey(a => a.CityID);

            // Relationship
            builder.HasOne<Country>(s => s.Country)
                .WithMany(g => g.Cities)
                .HasForeignKey(a => a.CountryId)
                .IsRequired(true);
        }
    }
}