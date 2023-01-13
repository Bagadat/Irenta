using DAL.DTO;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnlineStoreDbContext _context;

        public OrderRepository(OnlineStoreDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDTO> CreateAsync(OrderDTO entity)
        {
            var data = await _context.Orders.FirstOrDefaultAsync(e => e.Id == entity.Id);

            Validations.ValidateDataForExistence(data);

            if(entity.Products.Sum(e => e.Price) > 15000)
            {
                throw new DbUpdateException("Order amount should not exceed 15000.");
            }

            var order = await _context.Orders.AddAsync(entity);

            await _context.SaveChangesAsync();

            return order.Entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(d => d.Id == id);

            Validations.ValidateDataForAbsence(order);

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }

        public async Task<OrderDTO> GetAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(d => d.Id == id);

            Validations.ValidateDataForAbsence(order);

            return order;
        }

        public async Task<List<OrderDTO>> GetAllAsync() => await _context.Orders.ToListAsync();

        public async Task<OrderDTO> UpdateAsync(OrderDTO entity)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(a => a.Id == entity.Id);

            Validations.ValidateDataForAbsence(order);

            _context.Orders.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
