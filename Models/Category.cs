using System.ComponentModel.DataAnnotations;

namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
