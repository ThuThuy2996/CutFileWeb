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
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.IO;
using System.Collections;

namespace CutFileWeb.Controllers.Admin
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUploadRepository _uploadRepository;
        private readonly ICategoryRepository _categoryResponsitory;
        private readonly IBrandRepository _brandRepository;
        private IHostingEnvironment _environment;

        public ProductsController(IProductRepository productRepository, IUploadRepository uploadRepository, ICategoryRepository categoryRepository,
            IHostingEnvironment environment, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _uploadRepository = uploadRepository;
            _categoryResponsitory = categoryRepository;
            _brandRepository = brandRepository;
            _environment = environment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var product = await _productRepository.GetAllProductsAsync();

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> CreateAsync()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.categories = await _categoryResponsitory.GetAllCategoriesAsync();
            productViewModel.brands = await _brandRepository.GetAllBrandsAsync();
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
                    CategoryId = productViewModel.CategoryId,
                    ProductContentDetail = productViewModel.ProductContentDetail,
                    ProductPrice = productViewModel.ProductPrice,
                    ProductDescription = productViewModel.ProductDescription,
                    ProductImage = imageupload,
                    ProductFile = fileupload,
                    BrandId = productViewModel.BrandId

                };
                if (await _productRepository.Add(product))

                    return RedirectToAction(nameof(Create));
                else
                    ModelState.AddModelError("error", "Add new Category failed ! Please try again");
                return RedirectToAction(nameof(Create));
            }
            productViewModel.categories = await _categoryResponsitory.GetAllCategoriesAsync();
            productViewModel.brands = await _brandRepository.GetAllBrandsAsync();
            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var productViewModel = new ProductEditViewModel();
            var product = await _productRepository.GetProductById(id);
            productViewModel.ProductPrice = product.ProductPrice;
            productViewModel.ProductName = product.ProductName;
            productViewModel.ProductId = product.ProductId;
            productViewModel.BrandId = product.BrandId;
            productViewModel.CategoryId = product.CategoryId;           
            productViewModel.ProductContentDetail = product.ProductContentDetail;
            productViewModel.ProductDescription = product.ProductDescription;

            productViewModel.categories = await _categoryResponsitory.GetAllCategoriesAsync();
            productViewModel.brands = await _brandRepository.GetAllBrandsAsync();
            return View(productViewModel);

        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel productsAf)
        {
            if (id != productsAf.ProductId)
            {
                return NotFound();
            }
            var productBefore = await _productRepository.GetProductByIdAsyncNoTracking(id);

            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    ProductId = productsAf.ProductId,
                    ProductName = productsAf.ProductName,
                    CategoryId = productsAf.CategoryId,
                    ProductContentDetail = productsAf.ProductContentDetail,
                    ProductPrice = productsAf.ProductPrice,
                    ProductDescription = productsAf.ProductDescription,
                    BrandId = productsAf.BrandId,
                    ProductFile = productBefore.ProductFile,
                    ProductImage = productBefore.ProductImage

                };
                if (productsAf.ProductImage != null && productBefore.ProductImage != Path.Combine("Uploads", "Images", productsAf.ProductImage.FileName))
                {
                    _uploadRepository.DeleteFileAsync(productBefore.ProductImage);
                    product.ProductImage = await _uploadRepository.UploadImageAsync(productsAf.ProductImage);
                }
                if (productsAf.ProductFile != null && productBefore.ProductFile != Path.Combine("Uploads", "CutFiles", productsAf.ProductFile.FileName))
                {
                    _uploadRepository.DeleteFileAsync(productBefore.ProductFile);
                    product.ProductFile = await _uploadRepository.UploadFileAsync(productsAf.ProductFile);
                }
                if (await _productRepository.Update(product))
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(productsAf);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetProductByIdAsyncNoTracking(id);

       
            if (product == null)
            {
                return NotFound();
            }
        
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var product = await _productRepository.GetProductByIdAsyncNoTracking(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
