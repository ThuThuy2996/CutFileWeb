using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CutFileWeb.ViewModels
{
    public class CategoriesMenu
    {
        public int CategoryId { get; set; }   
        public string CategoryName { get; set; }      
        public List<ChildCategories>? ChildCategories { get; set; }
    }
    public class ChildCategories
    {
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public string ChildName { get; set; }
    }
}
