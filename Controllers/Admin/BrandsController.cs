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

namespace CutFileWeb.Controllers.Admin
{
    public class BrandsController : Controller
    {
        private readonly IBrandRepository _responsitory;

        public BrandsController(IBrandRepository responsitory)
        {
            _responsitory = responsitory;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return View(await _responsitory.GetAllBrandsAsync());
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int id)
        {          
            var brands = await _responsitory.GetBrandsByIdAsync(id);
            if (brands == null)
            {
                return NotFound();
            }

            return View(brands);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,BrandName,BrandDescription,BrandOrder")] Brand brands)
        {
            if (ModelState.IsValid)
            {
                if (await _responsitory.Add(brands))
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError("error", "Add new Brand failed ! Please try again");
            }
            return View(brands);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var brands = await _responsitory.GetBrandsByIdAsync(id);
            if (brands == null)
            {
                return NotFound();
            }
            return View(brands);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName,BrandDescription,BrandOrder")] Brand brands)
        {
            if (id != brands.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(await _responsitory.Update(brands))
               
                return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError("error", "Update Brand failed ! Please try again");
            }
            return View(brands);
        }

       // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var brands = await _responsitory.GetBrandsByIdAsync(id);          
            if (brands == null)
            {
                return NotFound();
            }

            return View(brands);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brands = await _responsitory.GetBrandsByIdAsync(id);
            if (brands != null)
            {
                await _responsitory.Delete(brands);
            }            
           
            return RedirectToAction(nameof(Index));
        }

    }
}
