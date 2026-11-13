using System.ComponentModel.DataAnnotations;

namespace LeAn_day3.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}