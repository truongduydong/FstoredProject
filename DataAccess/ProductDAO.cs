using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        FStoreDBAssignmentContext context = new FStoreDBAssignmentContext();

        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();



        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }


        public List<Product> GetProducts => context.Products.ToList();

        public List<Product> GetProductByName(string name)
        {
            List<Product> products = context.Products.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).ToList<Product>();
            return products;
        }

        public List<Product> GetProductByPrice(decimal minPrice, decimal maxPrice)
        {
            List<Product> products = context.Products.Where(p => p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice).ToList<Product>();
            return products;
        }

        public List<Product> GetProductByUnitInStock(int quantity)
        {
            List<Product> products = context.Products.Where(p=>p.UnitslnStock<=quantity).ToList<Product>();
            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = context.Products.SingleOrDefault<Product>(p => p.ProductId == productId);
            return product;
        }

        public void Add(Product product)
        {
            if (GetProductById(product.ProductId) == null)
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Try another id");
            }
        }

        public void Delete(int productId)
        {
            if (GetProductById(productId) != null)
            {
                context.Remove(context.Products.FirstOrDefault<Product>(p => p.ProductId == productId));
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid ID");
            }
        }

        public void Update(Product product)
        {
            Product pro = GetProductById(product.ProductId);
            if (pro != null)
            {
                pro.CategoryId = product.CategoryId;
                pro.ProductName = product.ProductName;
                pro.UnitslnStock = product.UnitslnStock;
                pro.UnitPrice = product.UnitPrice;
                pro.Weight = product.Weight;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Product does not exitst");
            }
        }


    }
}
