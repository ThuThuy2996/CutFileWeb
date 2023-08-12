using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CutFileWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string? CategoryName { get; set; }
        public int ParentId { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public int Active { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
