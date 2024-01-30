using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Repository.Models
{
    public class Artworks
    {
        //Id	Title	Description	Price	UserId	CreateOn	UpdateOn	Status	ReOrderQuantity	
        [Key]
        public int ArtworkId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool? Status { get; set; }
        public int? ReOrderQuantity { get; set; }

        public IdentityUser User { get; set; }
    }
}
