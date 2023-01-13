using Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public OrderStatuses Status { get; set; }

        [MaxLength(10)]
        public IEnumerable<ProductDTO> Products { get; set; } 
    }
}
