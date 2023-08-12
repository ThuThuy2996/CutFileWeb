using System.ComponentModel.DataAnnotations;

namespace CutFileWeb.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [Required]
        [StringLength(100)]
        public string? BrandName { get; set; }
        [StringLength(500)]
        public string? BrandDescription { get; set; }
        public int BrandOrder { get; set; }
        public ICollection<Product>? Products { get; set; }


    }
}
