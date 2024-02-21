using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<NotificationDTO> GetNotificationById(int id);
        Task<ResponseDTO> CreateNotification(NotificationDTO noti);
        Task<ResponseDTO> RemoveNotification(NotificationDTO noti);
    }
}
