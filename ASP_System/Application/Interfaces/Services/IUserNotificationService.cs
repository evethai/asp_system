using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserNotificationService
    {
        Task<IEnumerable<GetUserNotificationDTO>> GetNotificationByUserId(string userid);
        Task<ResponseDTO> CreateUserNotification(CreateUserNotificationDTO noti, string userid);
        Task<ResponseDTO> RemoveUserNotification(int id);
        Task<ResponseDTO> RemoveAllUserNotificationsByUserId(string userId);
    }
}
