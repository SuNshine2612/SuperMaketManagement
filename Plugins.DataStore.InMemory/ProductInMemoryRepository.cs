using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class ProductInMemoryRepository : IProductRepository
    {
        List<Product> products;

        public ProductInMemoryRepository()
        {
            products = new()
            {
                new Product() { ProductId = 1, CategoryId = 1, Name = "Vinamilk", Price = 1.99, Quantity = 120 },
                new Product() { ProductId = 2, CategoryId = 1, Name = "Dutch Lady Milk", Price = 1.70, Quantity = 80 },
                new Product() { ProductId = 3, CategoryId = 2, Name = "Chicken Parmigiana", Price = 12.80, Quantity = 150 },
                new Product() { ProductId = 4, CategoryId = 3, Name = "Chokos", Price = 5.66, Quantity = 50 },
            };
        }

        public void AddProduct(Product product)
        {
            if (products.Any(x => x.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))) return;

            if (products != null && products.Count > 0)
            {
                var maxID = products.Max(x => x.ProductId);
                product.ProductId = maxID++;
            }
            else
            {
                product.ProductId = 1;
            }
            products.Add(product);
        }

        public void DeleteProduct(int productID)
        {
            products?.Remove(GetProduct(productID));
        }

        public Product GetProduct(int productID)
        {
            return products?.FirstOrDefault(x => x.ProductId == productID);
        }

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public void UpdateProduct(Product product)
        {
            Product update = GetProduct(product.ProductId);
            if (update is not null)
            {
                update.Name = product.Name;
                update.CategoryId = product.CategoryId;
                update.Price = product.Price;
                update.Quantity = product.Quantity;
            }
        }
    }
}
