using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CutFileWeb.Data;
using CutFileWeb.Models;
using CutFileWeb.Interfaces;
using CutFileWeb.ViewModels;

namespace CutFileWeb.Controllers.Admin
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryResponsitory;

        public CategoriesController(ICategoryRepository categoryResponsitory)
        {
            _categoryResponsitory = categoryResponsitory;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryResponsitory.GetAllCategoriesAsync();

            var query = from p in categories
                        let order = p.ParentId == 0 ? p.CategoryId : p.ParentId
                        orderby order
                        select new Category
                        {
                            CategoryId = p.CategoryId,
                            CategoryName = p.CategoryName,
                            ParentId = p.ParentId,
                            Description = p.Description,
                            Active = p.Active,
                            Products = p.Products
                        };

            return View(query);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryResponsitory.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            var lstParent = await _categoryResponsitory.GetCategoriesByParentIdAsync(0);

            var lstParentView = (List<ParentCategories>)lstParent.Select(parent =>
            new ParentCategories
            {
                CategoryId = parent.CategoryId,
                CategoryName = parent.CategoryName
            });

            var categoriesView = new CategoriesViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ParentId = category.ParentId,
                Description = category.Description,
                Active = category.Active == 1 ? true : false,
                ParentCategories = lstParentView

            };

            return View(categoriesView);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            var lstParent = await _categoryResponsitory.GetCategoriesByParentIdAsync(0);
            var lstParentView = new List<ParentCategories>();

            if (lstParent != null && lstParent.Count() > 0)
            {
                foreach (var category in lstParent)
                {
                    lstParentView.Add(new ParentCategories { CategoryName = category.CategoryName, CategoryId = category.CategoryId });
                }
            }

            var categories = new CategoriesViewModel();
            categories.ParentCategories = lstParentView;

            return View(categories);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesViewModel categoriesViewModel)
        {
            var category = new Category();
            if (ModelState.IsValid)
            {
                category = new Category
                {
                    CategoryName = categoriesViewModel.CategoryName,
                    Active = categoriesViewModel.Active ? 1 : 0,
                    Description = categoriesViewModel.Description,
                    ParentId = categoriesViewModel.ParentId
                };
                if (await _categoryResponsitory.Add(category))

                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError("error", "Add new Category failed ! Please try again");
            }
            return View(categoriesViewModel);

        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryResponsitory.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            var lstParent = await _categoryResponsitory.GetCategoriesByParentIdAsync(0);
            var lstParentView = new List<ParentCategories>();

            if (lstParent != null && lstParent.Count() > 0)
            {
                foreach (var item in lstParent)
                {
                    lstParentView.Add(new ParentCategories { CategoryName = item.CategoryName, CategoryId = item.CategoryId });
                }
            }

            var categoriesView = new CategoriesViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ParentId = category.ParentId,
                Description = category.Description,
                Active = category.Active == 1 ? true : false,
                ParentCategories = lstParentView

            };

            return View(categoriesView);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriesViewModel categoriesVM)
        {
            if (id != categoriesVM.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var category = await _categoryResponsitory.GetByIdAsyncNoTracking(id);

                if (category == null)
                {
                    return View("Error");
                }

                var categoryUp = new Category
                {
                    CategoryId = categoriesVM.CategoryId,
                    CategoryName = categoriesVM.CategoryName,
                    ParentId = categoriesVM.ParentId,
                    Active = categoriesVM.Active ? 1 : 0,
                    Description = categoriesVM.Description
                };
                if (await _categoryResponsitory.Update(categoryUp))
                {
                    return RedirectToAction(nameof(Index));
                }


                else
                {
                    ModelState.AddModelError("", "Failed to edit club");
                    return View(categoriesVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View(categoriesVM);
            }
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryResponsitory.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var category = await _categoryResponsitory.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            if(category.Products != null && category.Products.Count > 0)
            {
                ViewBag.Message = "Doo not delete ! This category exists products";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _categoryResponsitory.Delete(category);
                return RedirectToAction(nameof(Index));
            }
        
        }
    }
}
