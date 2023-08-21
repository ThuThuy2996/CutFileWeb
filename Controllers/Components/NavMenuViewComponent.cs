using CutFileWeb.Interfaces;
using CutFileWeb.Models;
using CutFileWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CutFileWeb.Controllers.Components
{

    [ViewComponent(Name = "NavMenu")]
    public class NavMenuViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryResponsitory;
        public NavMenuViewComponent(ICategoryRepository categoryResponsitory)
        {
            _categoryResponsitory = categoryResponsitory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryResponsitory.GetParentsCategoriesAsync();
            var cateMenus = new List<CategoriesMenu>();
            if (categories != null && categories.Count() > 0)
            {
                foreach (var category in categories)
                {
                    var childCate = new List<ChildCategories>();
                   
                    var childs = await _categoryResponsitory.GetCategoriesByParentIdAsync(category.CategoryId);
                    if (childs != null && childs.Count() > 0)
                    {
                        foreach (var child in childs)
                        {
                            childCate.Add(new ChildCategories
                            {
                                ParentId = category.ParentId,
                                ChildId = child.CategoryId,
                                ChildName = child.CategoryName
                            });
                        }
                    }

                    var menu = new CategoriesMenu
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        ChildCategories = childCate
                    };
                    cateMenus.Add(menu);
                }

            }
            return View("_NavMenu", cateMenus);
        }
    }
}



