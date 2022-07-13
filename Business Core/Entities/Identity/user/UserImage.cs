using Business_Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Core.Entities
{
   public class UserImage
    {
        public int Id { get; set; }
        public string? URL { get; set; }
        public string? DescriptionHeader { get; set; }
        public string? Description { get; set; }
        public DateTime? DateAdded { get; set; }

        public bool IsMainPhoto { get; set; }
        public string? PublicId { get; set; }

        [ForeignKey("CustomIdentity")]

        public string? CustomIdentityId { get; set; }
        public CustomIdentity? CustomIdentity { get; set; }
    }
}
