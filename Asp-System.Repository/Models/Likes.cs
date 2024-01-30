using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Repository.Models
{
    public class Likes
    {
        //Id	ArtworkId	UserId
        [Key]
        public int Id { get; set; }
        public int? ArtworkId { get; set; }
        public int? UserId { get; set; }

        public Artworks Artwork { get; set; }
        public IdentityUser User { get; set; }
    }
}
