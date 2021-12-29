using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.DataStorePluginInterfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        Product GetProduct(int productID);
        void DeleteProduct(int productID);

        IEnumerable<Product> GetProductsByCategoryId(int categoryID);
    }
}
