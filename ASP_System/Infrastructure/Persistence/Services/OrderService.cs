using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<ResponseDTO> CreateOrder(OrderCreateDTO order) 
        {
            try
            {
                var newOrder = new Order
                {
                    Date = order.Date,
                    Code = order.Code,
                    ReOrderStatus = order.ReOrderStatus,

                };
                _unitOfWork.Repository<Order>().AddAsync(newOrder);
                _unitOfWork.Save();
                return Task.FromResult(new ResponseDTO { IsSuccess = true, Message = "Order added successfully", Data = order });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }

        }
        public async Task<OrderDTO> GetOrder(int id)
        {
            var Order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
            return _mapper.Map<OrderDTO>(Order);
        }


    }
}
