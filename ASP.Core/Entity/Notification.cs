using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Core.Models
{
    public class Notification
    {
        //Id	NofiticationId	Title	Description	Date	IsRead
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsRead { get; set; }

        public virtual ICollection<UserNofitication> UserNofitications { get; set;}
        
    }
}
