using Models.Enums;

namespace BLL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public OrderStatuses Status { get; set; }
    }
}
