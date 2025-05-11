namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
