using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CutFileWeb.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string? ProductName { get; set; }
     
        [Required]
        public int? ProductCategoryId { get; set; }
        [Required]
        public decimal? ProductPrice { get; set; }
        [StringLength(500)]
        public string? ProductDescription { get; set; }
        public string? ProductContentDetail { get; set; }
        [StringLength(200)]
        public string? ProductImage { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }

    }
}
