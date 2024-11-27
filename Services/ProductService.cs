using MachineTest.Data;
using MachineTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MachineTest.Services
{
    public class ProductService : IProductService
    {
        private readonly MyAppDbContext _context;

        public ProductService(MyAppDbContext _context)
        {
           this._context = _context;
        }

        public IEnumerable<Product> GetProducts(int page, int pageSize)
        {
            var products = _context.Products
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(p => p.Category)
                .ToList();
            return products;
        }
        public int GetTotalProductCount()
        {
            return _context.Products.Count(); 
        }
        public bool ProductExists(string productName)
        {
            return _context.Products.Any(c => c.ProductName == productName);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public Product GetProductById(int id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
        }

        public void EditProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

        }
       
    }
}
