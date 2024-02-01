using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Core.Models
{
    public class Follower
    {
        //FollowerId	UserId
        [Key]
        public Guid FollowerId { get; set; }
        public Guid? UserId { get; set; }

        public virtual IdentityUser User { get; set; }

    }
}
