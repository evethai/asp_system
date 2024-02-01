using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Core.Models
{
    public class ArtworkHasCategory
    {
        //Id	ArtworkId	CategoryId
        [Key]
        public Guid Id { get; set; }
        public Guid? ArtworkId { get; set; }
        public Guid? CategoryId { get; set; }

        public virtual Artwork Artwork { get; set; }
        public virtual Category Category { get; set; }

    }
}
