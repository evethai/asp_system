using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ArtworkImage
    {
        //Id	ArtworkId	Image
        [Key]
        public int Id { get; set; }
        public int? ArtworkId { get; set; }
        public string Image { get; set; }
        public virtual Artwork Artwork { get; set; }
    }
}
