using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CutFileWeb.ViewModels
{
    public class CategoriesViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required !")]
        public string? CategoryName { get; set; }
        public int ParentId { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public List<ParentCategories>? ParentCategories { get; set; }
    }
    public class CategoryView
    {
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required !")]
        public string? CategoryName { get; set; }
        public int ParentId { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
    }
    public class ParentCategories
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }     
    }

}
