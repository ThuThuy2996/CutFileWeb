using CutFileWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace CutFileWeb.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name is required !")]
        public string? ProductName { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is required !")]
        public Category category { get; set; }
        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Product Price is required !")]
        public decimal? ProductPrice { get; set; }       
        public string? ProductDescription { get; set; }

        [Display(Name = "Product Infomation")]
        [Required(ErrorMessage = "Product Infomation is required !")]
        public string? ProductContentDetail { get; set; }

        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Product Image is required !")]
        public IFormFile ProductImage { get; set; }
        [Display(Name = "Product File")]
        [Required(ErrorMessage = "Product File is required !")]
        public IFormFile ProductFile { get; set; }
        public Brand? Brand { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}
