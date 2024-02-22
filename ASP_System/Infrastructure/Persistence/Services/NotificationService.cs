using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ResponseDTO> CreateNotification(CreateNotificationDTO noti)
        {
            try
            {
                var newNotification = new Notification
                {
                    Title = noti.Title,
                    Description = noti.Description,
                    Date = DateTime.Now,
                    IsRead = false,
                };
                _unitOfWork.Repository<Notification>().AddAsync(newNotification);
                _unitOfWork.Save();
                return Task.FromResult(new ResponseDTO { IsSuccess = true, Message = "Notification added successfully", Data = noti });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllNotification()
        {
            var notiList = await _unitOfWork.Repository<Notification>().GetAllAsync();
            return _mapper.Map<List<NotificationDTO>>(notiList);
        }

        public async Task<NotificationDTO> GetNotificationById(int id)
        {
            var Noti = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);
            return _mapper.Map<NotificationDTO>(Noti);
        }

        public async Task<ResponseDTO> RemoveNotification(int id)
        {
            try
            {
                var existingNotification = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);

                if (existingNotification == null)
                {
                    return new ResponseDTO { IsSuccess = false, Message = "Notification not found" };
                }

                _unitOfWork.Repository<Notification>().DeleteAsync(existingNotification);
                _unitOfWork.Save();

                return new ResponseDTO { IsSuccess = true, Message = "Notification removed successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { IsSuccess = false, Message = ex.Message };
            }
        }

    }
}
