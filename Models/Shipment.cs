using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models
{
    public class Shipment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required]
        public DateTime ShipmentDate { get; set; }

        [StringLength(100)]
        public string TrackingNumber { get; set; }
    }
}
