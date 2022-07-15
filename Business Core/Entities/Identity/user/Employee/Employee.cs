using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities.Identity.user.Employee
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DathOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? HomeAddress { get; set; }
        public int Salary { get; set; }
        public string? Gender { get; set; }
        public string? EmployeeHireDate { get; set; }
        public string? UserId { get; set; }
        public virtual CustomIdentity? User { get; set; }
        public virtual ICollection<EmployeePayment>? EmployeePayments { get; set; }
    }

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(a => a.EmployeeID);

            // Relationship
            builder.HasOne<CustomIdentity>(s => s.User)
                .WithOne(g => g.Employee)
                .HasForeignKey<Employee>(a => a.UserId)
                .IsRequired(true);

        }
    }
}
