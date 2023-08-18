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
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUploadRepository _uploadRepository;
        private readonly ICategoryRepository _categoryResponsitory;

        public ProductsController(IProductRepository productRepository, IUploadRepository uploadRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _uploadRepository = uploadRepository;
            _categoryResponsitory = categoryRepository;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var product = _productRepository.GetAllProductsAsync();
            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> CreateAsync()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.category = new Category();
            productViewModel.categories = await _categoryResponsitory.GetAllCategoriesAsync();
            return View(productViewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {

                string fileupload = await _uploadRepository.UploadFileAsync(productViewModel.ProductFile);
                if (string.IsNullOrEmpty(fileupload))
                {
                    ModelState.AddModelError("error", "Upload file failed ! Please try again");
                    return View(productViewModel);
                }
                string imageupload = await _uploadRepository.UploadImageAsync(productViewModel.ProductImage);
                if (string.IsNullOrEmpty(fileupload))
                {
                    ModelState.AddModelError("error", "Upload Image failed ! Please try again");
                    return View(productViewModel);
                }

                var product = new Product
                {
                    ProductName = productViewModel.ProductName,
                   
                    CategoryId = productViewModel.category.CategoryId,
                    ProductContentDetail = productViewModel.ProductContentDetail,
                    ProductPrice = productViewModel.ProductPrice,
                    ProductDescription = productViewModel.ProductDescription,
                    ProductImage = imageupload,
                    ProductFile = fileupload,
                    BrandId = productViewModel.Brand?.BrandId
                };
                if (await _productRepository.Add(product))

                    return RedirectToAction(nameof(CreateAsync));
                else
                    ModelState.AddModelError("error", "Add new Category failed ! Please try again");
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Products == null)
            //{
            //    return NotFound();
            //}

            //var products = await _context.Products.FindAsync(id);
            //if (products == null)
            //{
            //    return NotFound();
            //}
            var product = _productRepository.GetAllProductsAsync();
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Product products)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(products);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!ProductsExists(products.ProductId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null || _context.Products == null)
            //{
            //    return NotFound();
            //}

            //var products = await _context.Products
            //    .FirstOrDefaultAsync(m => m.ProductId == id);
            //if (products == null)
            //{
            //    return NotFound();
            //}

            var product = _productRepository.GetAllProductsAsync();
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Products == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            //}
            //var products = await _context.Products.FindAsync(id);
            //if (products != null)
            //{
            //    _context.Products.Remove(products);
            //}

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
