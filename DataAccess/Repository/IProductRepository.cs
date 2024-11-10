using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int productId);
        List<Product> GetProductByName(string productname);
        List<Product> GetProductByPrice(decimal minPrice, decimal maxPrice);
        List<Product> GetProductsByUnitInStock(int quantity);
        void AddNew(Product product);
        void Delete(int productId);
        void Update(Product product);
    }
}
