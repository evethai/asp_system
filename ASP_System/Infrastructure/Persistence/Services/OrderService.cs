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
                    ArtworkId = order.ArtworkId
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

        public async Task<ResponseDTO> UpdateOrder(OrderUpdateDTO order)
        {
            try
            {
                var existingOrder = _unitOfWork.Repository<Order>().GetQueryable().FirstOrDefault(a => a.OrderId == order.OrderId);
                if (existingOrder == null)
                {
                    return (new ResponseDTO { IsSuccess = false, Message = "Order not found" });
                }
                existingOrder = submitCourseChange(existingOrder, order);
                await _unitOfWork.Repository<Order>().UpdateAsync(existingOrder);
                _unitOfWork.Save();
                return (new ResponseDTO { IsSuccess = true, Message = "Order updated successfully", Data = order });
            }
            catch (Exception ex)
            {
                return (new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
        }
        private Order submitCourseChange(Order existingOrder, OrderUpdateDTO order)
        {
            existingOrder.Code = order.Code;
            existingOrder.ArtworkId = order.ArtworkId;
            existingOrder.Date = order.Date;    
            existingOrder.ReOrderStatus = order.ReOrderStatus;
            
            return existingOrder;
        }

        public async Task<ResponseDTO> DeleteOrder(OrderDeleteDTO order)
        {
            try
            {
                var existingOrder = _unitOfWork.Repository<Order>().GetQueryable().FirstOrDefault(a => a.OrderId == order.OrderId);
                if (existingOrder == null)
                {
                    return (new ResponseDTO { IsSuccess = false, Message = "Order not found" });
                }
                existingOrder = submitCourse(existingOrder, order);
                await _unitOfWork.Repository<Order>().DeleteAsync(existingOrder);
                _unitOfWork.Save();
                return (new ResponseDTO { IsSuccess = true, Message = "Order deleted successfully", Data = order });
            }
            catch (Exception ex)
            {
                return (new ResponseDTO { IsSuccess = false, Message = ex.Message });
            }
        }

        private Order submitCourse(Order existingOrder, OrderDeleteDTO order)
        {
            existingOrder.Code = order.Code;
            existingOrder.ArtworkId = order.ArtworkId;
            existingOrder.Date = order.Date;
            existingOrder.ReOrderStatus = order.ReOrderStatus;

            return existingOrder;
        }
    }
}
