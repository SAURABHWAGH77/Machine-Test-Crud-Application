using MachineTest.Models;

namespace MachineTest.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(int page, int pageSize);


        bool CategoryExists(string categoryName);


        int GetTotalCategoriesCount();


        Category GetCategoryById(int id);


        void CreateCategory(Category category);


        void UpdateCategory(Category category);

        void DeleteCategory(int id);
    }
  
}
