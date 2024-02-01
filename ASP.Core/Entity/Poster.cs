using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Core.Models
{
    public class Poster
    {
        //Id	UserId	PackageId	QuantityPost
        [Key]
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? PackageId { get; set; }
        public int? QuantityPost { get; set; }

        public Package Package { get; set; }
        public IdentityUser User { get; set; }

    }
}
