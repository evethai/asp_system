using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Firebase.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserNotificationService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ResponseDTO> CreateUserNotification(CreateUserNotificationDTO noti, string userid)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userid);

                if (user == null)
                {
                    return await Task.FromResult(new ResponseDTO { IsSuccess = false, Message = "User not found" });
                }

                var newUserNotification = new UserNofitication
                {
                    ArtworkId = noti.ArtworkId,
                    NotificationId = noti.NotificationId,
                    User = user  // Assuming User property is related to ApplicationUser in UserNotification
                };

                _unitOfWork.Repository<UserNofitication>().AddAsync(newUserNotification);
                _unitOfWork.Save();  // Assuming SaveAsync is an asynchronous method

                return await Task.FromResult(new ResponseDTO { IsSuccess = true, Message = "Notification added successfully", Data = noti });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
        }


        public async Task<ResponseDTO> RemoveAllUserNotificationsByUserId(string userId)
        {
            try
            {
                var userNotifications = await _unitOfWork.Repository<UserNofitication>()
                    .GetQueryable()
                    .Where(noti => noti.User.Id == userId)
                    .ToListAsync();

                if (userNotifications == null || !userNotifications.Any())
                {
                    return new ResponseDTO { IsSuccess = false, Message = "No UserNotifications found for the specified UserId" };
                }

                foreach (var userNotification in userNotifications)
                {
                    _unitOfWork.Repository<UserNofitication>().DeleteAsync(userNotification);
                }
                _unitOfWork.Save();

                return new ResponseDTO { IsSuccess = true, Message = "All UserNotifications deleted successfully" };
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return new ResponseDTO { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<IEnumerable<GetUserNotificationDTO>> GetNotificationByUserId(string userId)
        {
            var userNotifications = await _unitOfWork.Repository<UserNofitication>()
            .GetQueryable()
            .Where(noti => noti.User.Id == userId)
            .Include(x => x.Artwork).ThenInclude(x=>x.ArtworkImages)
            .Include(x => x.Notification)
            .ToListAsync();

            List<GetUserNotificationDTO> dto = new List<GetUserNotificationDTO>();
            foreach (var notification in userNotifications)
            {
                Id = notification.Id,
                ArtworkTitle = notification.Artwork?.Title,
                NotificationTitle = notification.Notification?.Title,
                NotificationDescription = notification.Notification?.Description,
                isRead = notification.Notification.IsRead,
                artwordUrl = notification.Artwork.ArtworkImages.FirstOrDefault().Image
            }).ToList();
            return userNotificationDTOs;
        }



        public async Task<ResponseDTO> RemoveUserNotification(int id)
        {          
            try
            {
                var userNotification = await _unitOfWork.Repository<UserNofitication>().GetByIdAsync(id);

                if (userNotification == null)
                {
                    return new ResponseDTO { IsSuccess = false, Message = "UserNotification not found" };
                }

                await _unitOfWork.Repository<UserNofitication>().DeleteAsync(userNotification);
                _unitOfWork.Save(); // Assuming SaveAsync is an asynchronous method, if not, use Save()

                return new ResponseDTO { IsSuccess = true, Message = "UserNotification deleted successfully" };
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return new ResponseDTO { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
