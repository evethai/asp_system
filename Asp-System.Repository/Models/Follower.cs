using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Repository.Models
{
    public class Follower
    {
        //FollowerId	UserId
        [Key]
        public int FollowerId { get; set; }
        public int? UserId { get; set; }

        public virtual IdentityUser User { get; set; }

    }
}
