using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class PosterDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Package is required")]
        public int? PackageId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int? QuantityPost { get; set; }

        public Package Package { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public IdentityUser User { get; set; }
    }
    public class PosterAddDTO
    {
        public int? PackageId { get; set; }
        public int? QuantityPost { get; set; }
        public int UserId { get; set; }
    }
}
