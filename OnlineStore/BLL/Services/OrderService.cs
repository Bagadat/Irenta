using AutoMapper;
using BLL.Infrastructure;
using BLL.Models;
using BLL.Services.Interfaces;
using DAL.DTO;
using DAL.Repositories.Interfaces;
using FunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;

            _mapper = mapper;
        }

        public async Task<Order> CreateAsync(Order entity)
        {
            Validations.ValidateInput(entity);

            var order = await _mapper.Map<OrderDTO>(entity)
                                     .FeedTo(_orderRepository.CreateAsync)
                                     .FeedToAsync(_mapper.Map<Order>);

            return order;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var order = await _orderRepository.GetAsync(id);

            if(order.Status != OrderStatuses.Registered) 
            {
                throw new DbUpdateException("Order can be deleted only in the status Registered");
            }

            await _orderRepository.DeleteByIdAsync(id);
        }

        public async Task<Order> GetAsync(int id)
            => await _orderRepository.GetAsync(id)
                                     .FeedToAsync(_mapper.Map<Order>);

        public async Task<List<Order>> GetAllAsync() 
            => await _orderRepository.GetAllAsync()
                                     .FeedToAsync(_mapper.Map<List<Order>>);

        public async Task<Order> UpdateAsync(Order entity)
        {
            Validations.ValidateInput(entity);

            var order = await _orderRepository.GetAsync(entity.Id);

            if (order.Status != OrderStatuses.Registered)
            {
                throw new DbUpdateException("Order can be updated only in the status Registered");
            }

            return await _mapper.Map<OrderDTO>(entity)
                                .FeedTo(_orderRepository.UpdateAsync)
                                .FeedToAsync(_mapper.Map<Order>);
        }
    }
}
