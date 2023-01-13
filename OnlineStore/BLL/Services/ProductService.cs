using AutoMapper;
using BLL.Infrastructure;
using BLL.Models;
using BLL.Services.Interfaces;
using DAL.DTO;
using DAL.Repositories.Interfaces;
using FunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            Validations.ValidateInput(entity);

            var product = await _mapper.Map<ProductDTO>(entity)
                                       .FeedTo(_productRepository.CreateAsync)
                                       .FeedToAsync(_mapper.Map<Product>);

            return product;
        }

        public async Task DeleteByIdAsync(int id)
            => await _productRepository.DeleteByIdAsync(id);

        public async Task<Product> GetAsync(int id)
            => await _productRepository.GetAsync(id)
                                       .FeedToAsync(_mapper.Map<Product>);

        public async Task<List<Product>> GetAllAsync() 
            => await _productRepository.GetAllAsync()
                                       .FeedToAsync(_mapper.Map<List<Product>>);

        public async Task<Product> UpdateAsync(Product entity)
        {
            Validations.ValidateInput(entity);

            return await _mapper.Map<ProductDTO>(entity)
                                .FeedTo(_productRepository.UpdateAsync)
                                .FeedToAsync(_mapper.Map<Product>);
        }
    }
}
