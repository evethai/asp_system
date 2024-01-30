using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Repository.Models
{
    public class ArtworkHasCategory
    {
        //Id	ArtworkId	CategoryId
        [Key]
        public int Id { get; set; }
        public int? ArtworkId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Artworks Artwork { get; set; }
        public virtual Categorys Category { get; set; }

    }
}
