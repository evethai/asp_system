﻿using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrder(int id);
        Task<ResponseDTO> UpdateOrder(OrderUpdateDTO order);
        Task<ResponseDTO> CreateOrder(OrderCreateDTO order);
        Task<ResponseDTO> DeleteOrder(OrderDeleteDTO order);
        Task<OrderDTO> GetCode(int id);
        Task<OrderDTO> GetArtworkId(int id);
        
    }
}
