using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Core.Models
{
    public class Like
    {
        //Id	ArtworkId	UserId
        [Key]
        public Guid Id { get; set; }
        public Guid? ArtworkId { get; set; }
        public Guid? UserId { get; set; }

        public Artwork Artwork { get; set; }
        public IdentityUser User { get; set; }
    }
}
