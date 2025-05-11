namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO
{
    public class ShipmentDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string TrackingNumber { get; set; }
    }
}
