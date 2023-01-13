using Models.Enums;

namespace WebAPILayer.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public OrderStatuses Status { get; set; }
    }
}
