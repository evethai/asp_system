﻿using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Artwork
    {
        //Id	Title	Description	Price	UserId	CreateOn	UpdateOn	Status	ReOrderQuantity	
        [Key]
        public int ArtworkId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double? Price { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool? Status { get; set; }
        public int? ReOrderQuantity { get; set; }

        public virtual ICollection<ArtworkImage> ArtworkImages { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }
}