using MachineTest.Data;
using MachineTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MachineTest.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MyAppDbContext _context;
        public CategoryService(MyAppDbContext _context)
        {
            this._context = _context;
        }
         public IEnumerable<Category> GetCategories(int page, int pageSize)
        {
           var categories = _context.Categories
                .OrderBy(o => o.CategoryId)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .ToList();
            return categories;
        }

        public bool CategoryExists(string categoryName)
        {
            return _context.Categories.Any(c => c.CategoryName == categoryName);
        }
        public int GetTotalCategoriesCount()
        {
            return _context.Categories.Count();
        }
        public Category GetCategoryById(int id)
        {
            return _context.Categories.Find(id);
        }

       
        public void CreateCategory(Category category)
        {
          
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

       
        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

       
        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
