using System.ComponentModel.DataAnnotations;

namespace Day6_LeVanAn.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
