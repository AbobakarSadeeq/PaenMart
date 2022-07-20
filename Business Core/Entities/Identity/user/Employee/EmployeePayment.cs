using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Identity.user.Employee
{
    public class EmployeePayment
    {
        public int EmployeeMonthlyPaymentID { get; set; }
        public bool Payment { get; set; }
        public bool PaymentHistory { get; set; }
        public DateTime? Payment_At { get; set; }
        public string? PaymentStatus { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
    }
    public class EmployeePaymentConfiguration : IEntityTypeConfiguration<EmployeePayment>
    {
        public void Configure(EntityTypeBuilder<EmployeePayment> builder)
        {
            builder.HasKey(a => a.EmployeeMonthlyPaymentID);

            // Relationship
            builder.HasOne<Employee>(s => s.Employee)
                .WithMany(g => g.EmployeePayments)
                .HasForeignKey(a => a.EmployeeId)
                .IsRequired(true);

        }
    }
}
