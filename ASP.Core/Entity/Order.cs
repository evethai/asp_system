using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Core.Models
{
    public class Order
    {
        //OrderId	UserId	Date	ReOrderStatus	ArtworkId	Code
        [Key]
        public Guid OrderId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? Date { get; set; }
        public bool? ReOrderStatus { get; set; }
        public Guid? ArtworkId { get; set; }
        public string Code { get; set; }

        public IdentityUser User { get; set; }
        public Artwork Artwork { get; set; }   

    }
}
