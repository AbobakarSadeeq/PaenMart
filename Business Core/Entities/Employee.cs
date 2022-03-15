using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
