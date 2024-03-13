using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class UserNotificationDTO
    {
        [Key]
        public int Id { get; set; }
        public int? NotificationId { get; set; }
        public int? ArtworkId { get; set; }
        public string UserId { get; set; }
        public virtual Notification Notification { get; set; }
        public virtual Artwork Artwork { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class CreateUserNotificationDTO
    {
        [Required(ErrorMessage = "Title is required")]
        public int? NotificationId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public int? ArtworkId { get; set; }
    }
    public class GetUserNotificationDTO
    {
        public int Id { get; set; }
        public string ? ArtworkTitle { get; set; }
        public string ? NotificationTitle { get; set; }
        public string ? NotificationDescription { get; set; }
        public bool ? isRead { get; set; }
        public string? artwrokImage { get; set; }
    }
}
