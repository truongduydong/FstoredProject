using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void AddNew(Product product) => ProductDAO.Instance.Add(product);


        public void Delete(int productId) => ProductDAO.Instance.Delete(productId);


        public Product GetProductById(int productId) => ProductDAO.Instance.GetProductById(productId);


        public List<Product> GetProductByName(string productname) => ProductDAO.Instance.GetProductByName(productname);


        public List<Product> GetProductByPrice(decimal minimumPrice, decimal maximumPrice) => ProductDAO.Instance.GetProductByPrice(minimumPrice, maximumPrice);


        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProducts;


        public List<Product> GetProductsByUnitInStock(int quantity) => ProductDAO.Instance.GetProductByUnitInStock(quantity);


        public void Update(Product product) => ProductDAO.Instance.Update(product);


    }
}
