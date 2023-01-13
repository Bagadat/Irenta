using DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class OnlineStoreDbContext : DbContext
    {
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<ProductDTO> Products { get; set; }

        public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) : base(options)
        {

        }
    }
}
