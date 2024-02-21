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

        public Task<ResponseDTO> CreateNotification(NotificationDTO noti)
        {
            throw new NotImplementedException();
        }

        public async Task<NotificationDTO> GetNotificationById(int id)
        {
            var Noti = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);
            return _mapper.Map<NotificationDTO>(Noti);
        }

        public Task<ResponseDTO> RemoveNotification(NotificationDTO noti)
        {
            throw new NotImplementedException();
        }
    }
}
