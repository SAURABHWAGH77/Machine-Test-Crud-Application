using MachineTest.Models;

namespace MachineTest.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(int page, int pageSize);

       
        int GetTotalProductCount();

       
        bool ProductExists(string productName);

        
        void AddProduct(Product product);

        
        Product GetProductById(int id);

        
        void EditProduct(Product product);

        void DeleteProduct(int id);
    }
}
