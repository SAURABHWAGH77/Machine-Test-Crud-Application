using MachineTest.Data;
using MachineTest.Models;
using MachineTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MachineTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly MyAppDbContext _context;

        public ProductController(ProductService _productService, MyAppDbContext _context)
        {
            this._productService = _productService;
           this._context = _context;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var totalProducts = _productService.GetTotalProductCount();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            var products = _productService.GetProducts(page, pageSize);

            return View(products);
        }

        public ActionResult Create()
        {
           ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (_productService.ProductExists(product.ProductName))
            {
                ViewBag.Error = "Product Already Exist";
                ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                return View(product);
            }
            _productService.AddProduct(product);
                return RedirectToAction("Index");
           
        }
        public IActionResult ViewProduct(int id)
        {
          
            var product = _productService.GetProductById(id);

            
            if (product == null)
            {
                return NotFound();  
            }

            
            return View(product);
        }


        public ActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            
                _productService.EditProduct(product);
                return RedirectToAction("Index");
           
        }
        
        public ActionResult Delete(int id)
        {
            var category = _productService.GetProductById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
           
                _productService.DeleteProduct(id);
            
            return RedirectToAction("Index");
        }
    }
}
