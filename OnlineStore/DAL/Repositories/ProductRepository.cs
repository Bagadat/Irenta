using DAL.DTO;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineStoreDbContext _context;

        public ProductRepository(OnlineStoreDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDTO> CreateAsync(ProductDTO entity)
        {
            var data = await _context.Products.FirstOrDefaultAsync(e => e.Id == entity.Id);

            Validations.ValidateDataForExistence(data);

            var product = await _context.Products.AddAsync(entity);

            await _context.SaveChangesAsync();

            return product.Entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            Validations.ValidateDataForAbsence(product);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            Validations.ValidateDataForAbsence(product);

            return product;
        }

        public async Task<List<ProductDTO>> GetAllAsync() => await _context.Products.ToListAsync();

        public async Task<ProductDTO> UpdateAsync(ProductDTO entity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == entity.Id);

            Validations.ValidateDataForAbsence(product);

            _context.Products.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
