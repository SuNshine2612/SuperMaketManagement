using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketContext marketContext;

        public ProductRepository(MarketContext marketContext)
        {
            this.marketContext = marketContext;
        }


        public void AddProduct(Product product)
        {
            marketContext.Products.Add(product);
            marketContext.SaveChanges();
        }

        public void DeleteProduct(int productID)
        {
            var product = GetProduct(productID);

            if (product is null) return;

            marketContext.Products.Remove(product);
            marketContext.SaveChanges();
        }

        public Product GetProduct(int productID)
        {
            return marketContext.Products.Find(productID);
        }

        public IEnumerable<Product> GetProducts()
        {
            return marketContext.Products.ToList();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryID)
        {
            return marketContext.Products.Where(x => x.CategoryId == categoryID).ToList();
        }

        public void UpdateProduct(Product product)
        {
            var temp = GetProduct(product.ProductId);
            if (temp is null) return;

            temp.Name = product.Name;
            temp.CategoryId = product.CategoryId;
            temp.Price = product.Price;
            temp.Quantity = product.Quantity;

            marketContext.SaveChanges();
        }
    }
}
