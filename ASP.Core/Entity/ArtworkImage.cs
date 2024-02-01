using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Core.Models
{
    public class ArtworkImage
    {
        //Id	ArtworkId	Image
        [Key]
        public Guid Id { get; set; }
        public Guid? ArtworkId { get; set; }
        public string Image { get; set; }
        public virtual Artwork Artwork { get; set; }
    }
}
