using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Core.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public bool Status { get; set; }
        
        public virtual ICollection<UserNofitication> UserNofitications { get; set;}
        public virtual ICollection<Artwork> Artworks { get; set; }
        public virtual ICollection<Poster> Posters { get; set; }
        public virtual ICollection<Follower> Followers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
