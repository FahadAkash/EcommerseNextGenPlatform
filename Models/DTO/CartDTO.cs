namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
    }
}
