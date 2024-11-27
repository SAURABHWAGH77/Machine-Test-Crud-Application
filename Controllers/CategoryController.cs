using MachineTest.Data;
using MachineTest.Models;
using MachineTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachineTest.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(CategoryService _categoryService)
        {
           this._categoryService = _categoryService;
        }

        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var totalCategories = _categoryService.GetTotalCategoriesCount();
            var totalPages = (int)Math.Ceiling((double)totalCategories / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;

            var categories = _categoryService.GetCategories(page,pageSize);
            return View(categories);
        }

       

        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(Category category)
        {
            
            if(_categoryService.CategoryExists(category.CategoryName))
            {
                ViewBag.Error = "Category Already Exist";
                return View(category);
            }
            _categoryService.CreateCategory(category);
             return RedirectToAction("Index");
            
           
        }

        
        public ActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

       
        [HttpPost]
        public ActionResult Edit(Category category)
        {

            _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            
        }

      
        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            _categoryService.DeleteCategory(id);
            
            return RedirectToAction("Index");
        }
    }
}
