using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.DiscountDeal
{
    public class DiscountDeal
    {
        public int DiscountDealID { get; set; }
        public string? DealName { get; set; }
        public string?  DealStatus { get; set; }
        public int?  BackgroundJobId { get; set; }
        public DateTime? DealExpireAt { get; set; }
        public DateTime? DealCreatedAt { get; set; }
        public virtual ICollection<ProductDiscountDeal>?  ProductDiscountDeals { get; set; }
    }

    public class DiscountDealConfiguration : IEntityTypeConfiguration<DiscountDeal>
    {
        public void Configure(EntityTypeBuilder<DiscountDeal> builder)
        {
            builder.HasKey(a => a.DiscountDealID);
        }
    }
}
